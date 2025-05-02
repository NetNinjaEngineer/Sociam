using System.Net;
using System.Text;
using AutoMapper;
using FluentValidation;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Extensions;
using Sociam.Application.Features.Users.Commands.ChangeAccountEmail;
using Sociam.Application.Features.Users.Commands.ChangeAccountPassword;
using Sociam.Application.Features.Users.Commands.UpdateAvatar;
using Sociam.Application.Features.Users.Commands.UpdateCover;
using Sociam.Application.Features.Users.Commands.UpdateUserProfile;
using Sociam.Application.Features.Users.Commands.VerifyChangeEmail;
using Sociam.Application.Features.Users.Queries.GetUsernameSuggestions;
using Sociam.Application.Features.Users.Queries.IsEmailTaken;
using Sociam.Application.Features.Users.Queries.IsUsernameAvailable;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Services.Services;

public sealed class UserService(
    IMapper mapper,
    ICurrentUser currentUser,
    UserManager<ApplicationUser> userManager,
    IFileService fileService,
    IConfiguration configuration,
    IMailService mailService) : IUserService
{
    private readonly Random _random = new();

    public async Task<Result<ProfileDto?>> GetUserProfileAsync()
    {
        var profile = await userManager.GetUserProfileAsync(currentUser.Id, configuration);
        return profile == null ?
            Result<ProfileDto?>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UserNotExists) :
            Result<ProfileDto?>.Success(profile);
    }

    public async Task<Result<ProfileDto>> UpdateUserProfileAsync(UpdateUserProfileCommand command)
    {
        var existedUser = await userManager.FindByIdAsync(currentUser.Id);

        if (existedUser == null)
            return Result<ProfileDto>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UserNotExists);

        var validator = new UpdateUserProfileCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var mappedUser = mapper.Map(command, existedUser);

        await userManager.UpdateAsync(mappedUser);

        var updatedUserProfile = await GetUserProfileAsync();

        return Result<ProfileDto>.Success(updatedUserProfile.Value!);
    }

    public async Task<Result<string>> UpdateUserAvatarAsync(UpdateAvatarCommand command)
    {
        var existedUser = await userManager.FindByIdAsync(currentUser.Id);

        if (existedUser == null)
            return Result<string>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UserNotExists);

        var validator = new UpdateAvatarCommandValidator();

        await validator.ValidateAndThrowAsync(command);

        fileService.DeleteFileFromPath(existedUser.ProfilePictureUrl!, "Images");

        var uploadAvatarResult = await fileService.UploadFileAsync(command.Avatar, "Images");

        if (!uploadAvatarResult.uploaded)
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.FileUploadFailed);

        existedUser.ProfilePictureUrl = uploadAvatarResult.fileName;

        await userManager.UpdateAsync(existedUser);

        return Result<string>.Success($"{configuration["BaseApiUrl"]}/Uploads/Images/{existedUser.ProfilePictureUrl}");
    }

    public async Task<Result<string>> UpdateUserCoverAsync(UpdateCoverCommand command)
    {
        var existedUser = await userManager.FindByIdAsync(currentUser.Id);

        if (existedUser == null)
            return Result<string>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UserNotExists);

        var validator = new UpdateCoverCommandValidator();

        await validator.ValidateAndThrowAsync(command);

        fileService.DeleteFileFromPath(existedUser.CoverPhotoUrl!, "Images");

        var uploadCoverResult = await fileService.UploadFileAsync(command.Cover, "Images");

        if (!uploadCoverResult.uploaded)
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.FileUploadFailed);

        existedUser.CoverPhotoUrl = uploadCoverResult.fileName;

        await userManager.UpdateAsync(existedUser);

        return Result<string>.Success($"{configuration["BaseApiUrl"]}/Uploads/Images/{existedUser.CoverPhotoUrl}");
    }

    public async Task<Result<bool>> ChangeAccountEmailAsync(ChangeAccountEmailCommand command)
    {
        var existedUser = await userManager.FindByIdAsync(currentUser.Id);

        if (existedUser == null)
            return Result<bool>.Failure(HttpStatusCode.Unauthorized);

        var isCorrectPassword = await userManager.CheckPasswordAsync(existedUser, command.OldEmailPassword);

        if (!isCorrectPassword)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.WrongPassword);

        var validator = new ChangeAccountEmailCommandValidator();

        await validator.ValidateAndThrowAsync(command);

        // Check if the new email already exists in the database
        var existingUserWithEmail = await userManager.FindByEmailAsync(command.NewEmail);
        if (existingUserWithEmail != null && existingUserWithEmail.Id != existedUser.Id)
        {
            return Result<bool>.Failure(HttpStatusCode.BadRequest, "The email is already in use by another account.");
        }

        var emailResult = await userManager.SetEmailAsync(existedUser, command.NewEmail);

        if (!emailResult.Succeeded)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, emailResult.Errors.First().Description);

        var token = await userManager.GenerateUserTokenAsync(existedUser, "Email", "Change account email");

        existedUser.Code = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        existedUser.CodeExpiration =
            DateTimeOffset.UtcNow.AddMinutes(Convert.ToDouble(configuration["AuthCodeExpirationInMinutes"]));
        existedUser.EmailConfirmed = false;

        await userManager.UpdateAsync(existedUser);

        await mailService.SendEmailAsync(new EmailMessage
        {
            Message = $@"
              <h1>Verify Your Email</h1>
              <p>Hello {existedUser.FirstName ?? existedUser.UserName},</p>
              <p>Use this code to change your email:</p>
              <div class='code'>{token}</div>
              <p>It expires in {configuration["AuthCodeExpirationInMinutes"]} minutes.</p>
              <div class='footer'>Sociam Team</div>",
            To = command.NewEmail,
            Subject = "Verify Change Email"
        });

        return Result<bool>.Success(true);
    }

    public async Task<Result<string>> VerifyChangeAccountEmailAsync(VerifyChangeEmailCommand command)
    {
        var validator = new VerifyChangeEmailCommandValidator();

        await validator.ValidateAndThrowAsync(command);

        var existedUser = await userManager.FindByIdAsync(currentUser.Id);
        if (existedUser == null)
            return Result<string>.Failure(HttpStatusCode.Unauthorized);
        if (existedUser.Code == null || existedUser.CodeExpiration == null)
            return Result<string>.Failure(HttpStatusCode.BadRequest, "The code is invalid.");
        if (existedUser.CodeExpiration < DateTimeOffset.UtcNow)
            return Result<string>.Failure(HttpStatusCode.BadRequest, "The code has expired.");
        if (Encoding.UTF8.GetString(Convert.FromBase64String(existedUser.Code)) != command.Code)
            return Result<string>.Failure(HttpStatusCode.BadRequest, "The code is invalid.");
        existedUser.EmailConfirmed = true;
        existedUser.Code = null;
        existedUser.CodeExpiration = null;
        await userManager.UpdateAsync(existedUser);
        return Result<string>.Success("Your email has been changed successfully.");
    }

    public async Task<Result<bool>> ChangeAccountPasswordAsync(ChangeAccountPasswordCommand command)
    {
        var validator = new ChangeAccountPasswordCommandValidator();

        await validator.ValidateAndThrowAsync(command);

        var existedUser = await userManager.FindByIdAsync(currentUser.Id);
        if (existedUser == null)
            return Result<bool>.Failure(HttpStatusCode.Unauthorized);

        var isCorrectPassword = await userManager.CheckPasswordAsync(existedUser, command.OldPassword);
        if (!isCorrectPassword)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.WrongPassword);

        var passwordResult = await userManager.ChangePasswordAsync(existedUser, command.OldPassword, command.NewPassword);

        return !passwordResult.Succeeded ?
            Result<bool>.Failure(HttpStatusCode.BadRequest, passwordResult.Errors.First().Description) :
            Result<bool>.Success(true);
    }

    public async Task<Result<IReadOnlyList<TrustedDeviceDto>>> GetUserTrustedDevicesAsync()
    {
        var authenticatedUser = await userManager.Users
            .Include(u => u.TrustedDevices)
            .FirstOrDefaultAsync(u => u.Id == currentUser.Id);

        if (authenticatedUser == null)
            return Result<IReadOnlyList<TrustedDeviceDto>>.Failure(HttpStatusCode.Unauthorized);

        var trustedDevices = authenticatedUser.TrustedDevices.Select(d =>
            new TrustedDeviceDto(
                d.Id,
                $"{d.BrowserName} {d.BrowserVersion}",
                $"{d.OsName} {d.OsVersion} {d.OsPlatform}",
                d.IpAddress,
                d.Location,
                d.IsActive,
                d.CreatedAt.ConvertToUserLocalTimeZone(authenticatedUser.TimeZoneId).ToString("MMM d, yyyy, hh:mm tt"),
                d.DeviceName,
                d.LastLogin.Humanize(),
                d.Model,
                d.Brand,
                d.ExpiryDate.ConvertToUserLocalTimeZone(authenticatedUser.TimeZoneId))).ToList();

        return Result<IReadOnlyList<TrustedDeviceDto>>.Success(trustedDevices);
    }

    public async Task<Result<bool>> IsUsernameAvailableAsync(IsUsernameAvailableQuery query)
    {
        var exists = await IsUsernameExists(query.Username);

        return !exists ? Result<bool>.Success(true) : Result<bool>.Failure(HttpStatusCode.Conflict);
    }

    private async Task<bool> IsUsernameExists(string username)
    {
        var exists = await userManager.Users.AnyAsync(
            u => u.UserName != null && u.UserName.ToLower() == username.ToLower());
        return exists;
    }

    public async Task<Result<List<string>>> GetUsernameSuggestionsAsync(GetUsernameSuggestionsQuery query)
    {
        var username = query.Username;

        if (string.IsNullOrEmpty(username))
            return Result<List<string>>.Failure(HttpStatusCode.BadRequest);

        var suggestions = new List<string>();

        suggestions.AddRange([
                $"{username}{_random.Next(1, 1000)}",
                $"{username}{DateTime.UtcNow.Year}",
                $"{username}_{GetRandomLetters(2)}",

                $"the_{username}",
                $"real_{username}",
                $"{username}_official",
                $"official_{username}",
                $"{username}_verified",
                $"{username}.official",

                $"@{username}",
                $"{username}_{_random.Next(1, 100)}",
                $"{GetRandomLetters(1)}{username}",
                $"{username}.{GetRandomLetters(2)}",
                $"x{username}",

                $"{username}_social",
                $"{username}.io",
                $"{username}_app",
                $"{username}_original",
                $"its_{username}",
                $"just_{username}",

                $"i_am_{username}",
                $"{username}_world",
                $"{username}_here",
                $"{username}_now",
                $"try_{username}",

                $"{username}_global",
                $"{username}_{GetRandomCountryCode()}",

                $"{username}{DateTime.UtcNow.Month}{DateTime.UtcNow.Day}",

                $"{username}{GetEmoji()}",
                $"{GetEmoji()}{username}",
                $"{username}_{_random.Next(1, 10)}_{_random.Next(1, 10)}",
                $"{GetShortUuid()}_{username}"
        ]);

        var availableSuggestions = new List<string>();

        foreach (var suggestion in suggestions)
        {
            if (await IsUsernameExists(suggestion)) continue;

            availableSuggestions.Add(suggestion);

            if (availableSuggestions.Count >= suggestions.Count)
            {
                break;
            }
        }

        var randomSuggestions = availableSuggestions.OrderBy(_ => _random.Next()).Take(10).ToList();
        if (availableSuggestions.Count >= suggestions.Count)
            return Result<List<string>>.Success(randomSuggestions);

        var attemptsLeft = suggestions.Count;

        while (availableSuggestions.Count < suggestions.Count && attemptsLeft > 0)
        {
            var newSuggestion = $"{username}{_random.Next(1000, 10000)}";

            if (!availableSuggestions.Contains(newSuggestion) && !await IsUsernameExists(newSuggestion))
            {
                availableSuggestions.Add(newSuggestion);
            }

            attemptsLeft--;
        }

        return Result<List<string>>.Success(randomSuggestions);
    }

    private string GetRandomLetters(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    private string GetRandomCountryCode()
    {
        string[] countryCodes = ["us", "uk", "ca", "au", "de", "fr", "jp", "br", "in", "es", "it", "nl", "se", "sg", "eg"];
        return countryCodes[_random.Next(countryCodes.Length)];
    }

    private string GetEmoji()
    {
        string[] emojiOptions = ["⭐", "🔥", "💯", "✨", "📱", "💫", "🌟", "🚀", "💪", "👑"];
        return emojiOptions[_random.Next(emojiOptions.Length)];
    }

    private string GetShortUuid()
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }

    public async Task<Result<bool>> IsEmailTakenAsync(IsEmailTakenQuery query)
    {
        var exists = await userManager.Users.AnyAsync(
            u => u.Email != null && u.Email.ToLower() == query.Email.ToLower());

        return !exists ? Result<bool>.Success(false) : Result<bool>.Failure(HttpStatusCode.Conflict, true);
    }

}