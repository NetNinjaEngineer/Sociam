using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Extensions;
using Sociam.Application.Features.Users.Commands.UpdateAvatar;
using Sociam.Application.Features.Users.Commands.UpdateCover;
using Sociam.Application.Features.Users.Commands.UpdateUserProfile;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities.Identity;
using System.Net;

namespace Sociam.Services.Services;

public sealed class UserService(
    IMapper mapper,
    ICurrentUser currentUser,
    UserManager<ApplicationUser> userManager,
    IFileService fileService,
    IConfiguration configuration) : IUserService
{
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
}