﻿using AutoMapper;
using FluentValidation;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QRCoder;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Features.Auth.Commands.ConfirmEmail;
using Sociam.Application.Features.Auth.Commands.ConfirmEnable2FaCommand;
using Sociam.Application.Features.Auth.Commands.ConfirmForgotPasswordCode;
using Sociam.Application.Features.Auth.Commands.Disable2Fa;
using Sociam.Application.Features.Auth.Commands.Enable2Fa;
using Sociam.Application.Features.Auth.Commands.EnableMfa;
using Sociam.Application.Features.Auth.Commands.ForgetPassword;
using Sociam.Application.Features.Auth.Commands.LoginUser;
using Sociam.Application.Features.Auth.Commands.RefreshToken;
using Sociam.Application.Features.Auth.Commands.Register;
using Sociam.Application.Features.Auth.Commands.RevokeToken;
using Sociam.Application.Features.Auth.Commands.SendConfirmEmailCode;
using Sociam.Application.Features.Auth.Commands.SignInGoogle;
using Sociam.Application.Features.Auth.Commands.ValidateToken;
using Sociam.Application.Features.Auth.Commands.Verify2FaCode;
using Sociam.Application.Features.Auth.Commands.VerifyMfa;
using Sociam.Application.Features.Auth.Commands.VerifyMfaLogin;
using Sociam.Application.Features.Auth.Queries.GetAccessToken;
using Sociam.Application.Helpers;
using Sociam.Application.Helpers.GeoLocation;
using Sociam.Application.Helpers.IpInfo;
using Sociam.Application.Interfaces.Services;
using Sociam.Application.Interfaces.Services.Models;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;
using Sociam.Infrastructure.Persistence;
using Sociam.Persistence.Clients;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Sociam.Services.Services;

public sealed class AuthService(
    IMemoryCache memoryCache,
    ILogger<AuthService> logger,
    IOptions<AuthOptions> authOptions,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ApplicationDbContext context,
    IMapper mapper,
    IMailService mailService,
    IConfiguration configuration,
    ITokenService tokenService,
    IHttpContextAccessor contextAccessor,
    IFileService fileService,
    IOptions<JwtSettings> jwtOptions,
    IIpInfoApi ipInfoApi,
    IGeoLocationApi geoLocationApi,
    IOptions<IpGeoLocationOptions> geoLocationOptions,
    IOptions<IpInfoOptions> ipInfoOptions) : IAuthService
{
    private const string CacheKeyPrefix = "GoogleToken_";
    private readonly AuthOptions _authenticationOptions = authOptions.Value;
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    private readonly MemoryCacheEntryOptions _timeZoneCacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
    };
    private readonly IpInfoOptions _ipInfo = ipInfoOptions.Value;
    private readonly IpGeoLocationOptions _ipGeoLocation = geoLocationOptions.Value;

    public async Task<Result<GoogleUserProfile?>> VerifyAndGetUserProfileAsync(SignInGoogleCommand command)
    {
        var validator = new SignInGoogleCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        GoogleJsonWebSignature.ValidationSettings validationSettings = new()
        {
            Audience = [_authenticationOptions.GoogleOptions.ClientId]
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(command.IdToken, validationSettings);

        var cacheKey = $"{CacheKeyPrefix}{payload.Subject}";
        if (memoryCache.TryGetValue(cacheKey, out GoogleUserProfile? userProfile))
        {
            logger.LogInformation($"Get GoogleUser Info From Cache: {JsonSerializer.Serialize(userProfile)}");

            return Result<GoogleUserProfile?>.Success(userProfile);
        }

        var profile = new GoogleUserProfile(
            Email: payload.Email,
            Name: payload.Name,
            Picture: payload.Picture,
            FirstName: payload.GivenName,
            LastName: payload.FamilyName,
            GoogleId: payload.Subject,
            Locale: payload.Locale,
            EmailVerified: payload.EmailVerified,
            HostedDomain: payload.HostedDomain,
            Expires: TimeSpan.FromSeconds(Convert.ToDouble(payload.ExpirationTimeSeconds)));

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

        memoryCache.Set(cacheKey, profile, cacheOptions);

        var existingUser = await userManager.FindByEmailAsync(payload.Email);

        if (existingUser != null)
        {
            return Result<GoogleUserProfile?>.Success(profile);
        }

        existingUser = new ApplicationUser()
        {
            Email = payload.Email,
            FirstName = payload.GivenName,
            LastName = payload.FamilyName,
            EmailConfirmed = payload.EmailVerified,
            ProfilePictureUrl = payload.Picture,
            UserName = payload.Email,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var createResult = await userManager.CreateAsync(existingUser);

        if (!createResult.Succeeded)
            return Result<GoogleUserProfile?>.Failure(
                statusCode: HttpStatusCode.UnprocessableEntity,
                message: "One or more errors happened",
                errs: createResult.Errors.Select(e => $"{e.Code}: {e.Description}").ToList());

        var loginResult = await userManager.AddLoginAsync(existingUser,
            new UserLoginInfo("Google", existingUser.Email, existingUser.FirstName));

        if (loginResult.Succeeded)
        {
            return Result<GoogleUserProfile?>.Success(profile);
        }

        return Result<GoogleUserProfile?>.Failure(
            statusCode: HttpStatusCode.UnprocessableEntity,
            message: "One or more errors happened when tring to login !!!",
            errs: loginResult.Errors.Select(e => $"{e.Code} : {e.Description}").ToList());
    }

    public async Task<Result<RegisterResponseDto>> RegisterAsync(RegisterCommand command)
    {
        var registerCommandValidator = new RegisterCommandValidator();
        await registerCommandValidator.ValidateAndThrowAsync(command);

        var user = mapper.Map<ApplicationUser>(command);

        var (_, uploadedFileName) = await fileService.UploadFileAsync(command.Picture, "Images");
        user.ProfilePictureUrl = uploadedFileName;
        var timeZoneId = await GetUserTimeZoneAsync();
        user.TimeZoneId = timeZoneId ?? TimeZoneInfo.Local.Id;

        var result = await userManager.CreateAsync(user, command.Password);

        await userManager.AddToRoleAsync(user, AppConstants.Roles.User);

        return !result.Succeeded
            ? Result<RegisterResponseDto>.Failure(
                HttpStatusCode.BadRequest,
                DomainErrors.Users.UnableToCreateAccount,
                [result.Errors.Select(e => e.Description).FirstOrDefault() ?? string.Empty])
            : Result<RegisterResponseDto>.Success(new RegisterResponseDto(user.Id, true));
    }

    private async Task<string?> GetUserTimeZoneAsync()
    {
        var userIpAddress = contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        if (string.IsNullOrEmpty(userIpAddress))
            return null;

        if (memoryCache.TryGetValue(userIpAddress, out string? cachedTimeZone))
            return cachedTimeZone;

        var timeZone = await FetchTimeZoneFromApiAsync(userIpAddress);
        if (!string.IsNullOrEmpty(timeZone))
            memoryCache.Set(userIpAddress, timeZone, _timeZoneCacheOptions);

        return timeZone;
    }

    private async Task<string?> FetchTimeZoneFromApiAsync(string ipAddress)
    {
        try
        {
            var response = await ipInfoApi.GetIpInfoAsync(ipAddress, _ipInfo.Token);
            if (!string.IsNullOrEmpty(response?.Timezone))
                return response.Timezone;

            var geoLocationResponse = await geoLocationApi.GetGeoLocationAsync(_ipGeoLocation.ApiKey, ipAddress);
            return geoLocationResponse?.TimeZone.Name;
        }
        catch (Exception)
        {
            logger.LogInformation("Local ip address: {0}", ipAddress);
            return null;
        }
    }


    public async Task<Result<SendCodeConfirmEmailResponseDto>> SendConfirmEmailCodeAsync(
        SendConfirmEmailCodeCommand command)
    {
        var confirmEmailValidator = new SendConfirmEmailCodeCommandValidator();
        await confirmEmailValidator.ValidateAndThrowAsync(command);

        var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var user = await userManager.FindByEmailAsync(command.Email);

            if (user is null)
                return Result<SendCodeConfirmEmailResponseDto>.Failure(
                    HttpStatusCode.NotFound, DomainErrors.Users.UnkownUser);

            if (await userManager.IsEmailConfirmedAsync(user))
                return Result<SendCodeConfirmEmailResponseDto>.Failure(
                    HttpStatusCode.Conflict,
                    DomainErrors.Users.AlreadyEmailConfirmed);

            var authenticationCode = await userManager.GenerateUserTokenAsync(user, "Email", "Confirm User Email");
            var encodedAuthenticationCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationCode));

            user.Code = encodedAuthenticationCode;
            user.CodeExpiration = DateTimeOffset.UtcNow.AddMinutes(
                minutes: Convert.ToDouble(configuration[AppConstants.AuthCodeExpireKey]!)
            );

            var identityResult = await userManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors
                    .Select(e => e.Description)
                    .ToList();

                return Result<SendCodeConfirmEmailResponseDto>.Failure(
                    HttpStatusCode.BadRequest,
                    DomainErrors.Users.UnableToUpdateUser, errors);
            }

            var emailMessage = new EmailMessage
            {
                To = command.Email,
                Subject = "Activate your Sociam Account",
                Message = $@"
                    Welcome to Sociam!

                    Thank you for registering with Sociam. To activate your account, please use the following code:

                    {authenticationCode}

                    This code will expire in {configuration[AppConstants.AuthCodeExpireKey]} minutes.

                    If you did not request this registration, please ignore this email.

                    Best regards,
                    The Sociam Team"
            };


            await mailService.SendEmailAsync(emailMessage);


            await transaction.CommitAsync();
            return Result<SendCodeConfirmEmailResponseDto>.Success(
                    SendCodeConfirmEmailResponseDto.ToResponse(user.CodeExpiration.Value.ConvertToUserLocalTimeZone(user.TimeZoneId)),
                    AppConstants.ConfirmEmailCodeSentSuccessfully
            );
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result<SendCodeConfirmEmailResponseDto>.Failure(HttpStatusCode.BadRequest, ex.Message);
        }
    }


    private static RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);
        return new RefreshToken()
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTimeOffset.UtcNow.AddDays(10),
            CreatedOn = DateTimeOffset.UtcNow
        };
    }

    private async Task<Result<ApplicationUser>> CheckIfUserHasAssignedToRefreshToken(
        string refreshToken)
    {
        var user = await userManager.Users.SingleOrDefaultAsync(x =>
            x.RefreshTokens != null && x.RefreshTokens.Any(token => token.Token == refreshToken));
        return user is null
            ? Result<ApplicationUser>.Failure(HttpStatusCode.NotFound, "Invalid Token")
            : Result<ApplicationUser>.Success(user);
    }


    public async Task<Result<string>> ForgotPasswordAsync(ForgetPasswordCommand command)
    {
        var validator = new ForgetPasswordCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<string>.Failure(
                HttpStatusCode.NotFound, "User not found");

        //code and Expiration 
        var decoded = await userManager.GenerateUserTokenAsync(user, "Email", "Generate Code");
        var authCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(decoded));
        user.Code = authCode;
        user.CodeExpiration =
            DateTimeOffset.UtcNow.AddMinutes(Convert.ToDouble(configuration[AppConstants.AuthCodeExpireKey]));

        //Update user
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.UnableToUpdateUser);

        var emailMessage = new EmailMessage
        {
            To = command.Email,
            Subject = "Sociam - Reset Password Code",
            Message = $@"
                    Password Reset Code

                    Hello,

                    To reset your Sociam account password, please use the following code:

                    {decoded}

                    This code will expire in {configuration[AppConstants.AuthCodeExpireKey]} minutes.

                    If you did not request a password reset, please ignore this email.

                    Best regards,
                    The Sociam Team"
        };


        await mailService.SendEmailAsync(emailMessage);

        return Result<string>.Success("Password reset code has been sent to your email.");
    }

    public async Task<Result<string>> ConfirmForgotPasswordCodeAsync(ConfirmForgotPasswordCodeCommand command)
    {
        var validator = new ConfirmForgotPasswordCodeCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.UserNotFound);

        var decodedAuthCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code!));
        if (decodedAuthCode != command.Code)
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.InvalidAuthCode);

        if (DateTimeOffset.UtcNow > user.CodeExpiration)
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.CodeExpired);

        await userManager.RemovePasswordAsync(user);
        var result = await userManager.AddPasswordAsync(user, command.NewPassword);

        if (result.Succeeded) return Result<string>.Success("Reset Password Successfuly.");

        return Result<string>.Failure(HttpStatusCode.UnprocessableEntity);


    }

    public async Task<Result<string>> Enable2FaAsync(Enable2FaCommand command)
    {
        var validator = new Enable2FaCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await userManager.FindByEmailAsync(command.Email);

        if (user == null)
            return Result<string>.Failure(HttpStatusCode.BadRequest,
                string.Format(DomainErrors.Users.UserNotFound, command.Email));

        var code = await userManager.GenerateTwoFactorTokenAsync(user, command.TokenProvider.ToString());

        await signInManager.TwoFactorSignInAsync(code, command.TokenProvider.ToString(), false, true);

        if (command.TokenProvider == TokenProvider.Email)
            // send code via user email
            await mailService.SendEmailAsync(
                new EmailMessage()
                {
                    To = command.Email,
                    Subject = "Enable Sociam 2FA",
                    Message = $@"
                    Enable 2-Factor Authentication

                    To enhance the security of your Sociam account, we recommend enabling 2-factor authentication (2FA).

                    Use this code to enable 2FA on your account: {code}

                    This code will expire in 5 minutes.

                    If you did not request this 2FA activation, please ignore this email.

                    Best regards,
                    The Sociam Team"
                });



        else if (command.TokenProvider == TokenProvider.Phone)
        {
            // handle send via phone
        }

        return Result<string>.Success(AppConstants.TwoFactorCodeSent);
    }

    public async Task<Result<string>> ConfirmEnable2FaAsync(ConfirmEnable2FaCommand command)
    {
        var validator = new ConfirmEnable2FaCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<string>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UnkownUser);

        // verify 2fa code
        var providers = await userManager.GetValidTwoFactorProvidersAsync(user);

        if (providers.Contains(TokenProvider.Email.ToString()))
        {
            var verified =
                await userManager.VerifyTwoFactorTokenAsync(user, TokenProvider.Email.ToString(), command.Code);

            if (!verified)
                return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.Invalid2FaCode);

            // code is verified update status of 2FA

            await userManager.SetTwoFactorEnabledAsync(user, true);

            await mailService.SendEmailAsync(
                new EmailMessage()
                {
                    Subject = "Sociam 2FA Setup Complete",
                    To = user.Email!,
                    Message = $@"
                        2-Factor Authentication Enabled

                        Congratulations! Your 2-factor authentication (2FA) has been successfully enabled for your Sociam account.

                        Your account is now more secure, and you'll be required to enter a one-time code when logging in.

                        Best regards,
                        The Sociam Team"
                });



            return Result<string>.Success(AppConstants.TwoFactorEnabled);
        }

        return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.InvalidTokenProvider);
    }

    public async Task<Result<SignInResponseDto>> Verify2FaCodeAsync(Verify2FaCodeCommand command)
    {
        var validator = new Verify2FaCodeCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var userEmail =
            Encoding.UTF8.GetString(
                Convert.FromBase64String(contextAccessor.HttpContext!.Request.Cookies["userEmail"]!));
        var appUser = await userManager.FindByEmailAsync(userEmail);

        if (appUser == null) return Result<SignInResponseDto>.Failure(HttpStatusCode.Unauthorized);

        var verified = await userManager.VerifyTwoFactorTokenAsync(appUser, "Email", command.Code);

        if (!verified)
            return Result<SignInResponseDto>.Failure(HttpStatusCode.BadRequest,
                DomainErrors.Users.Invalid2FaCode);

        await signInManager.SignInAsync(appUser, isPersistent: true);

        var response = await CreateLoginResponseAsync(userManager, appUser);

        return Result<SignInResponseDto>.Success(response);
    }

    public async Task<Result<string>> Disable2FaAsync(Disable2FaCommand command)
    {
        var validator = new Disable2FaCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await userManager.FindByEmailAsync(command.Email);

        if (user == null)
            return Result<string>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Users.UserNotFound, command.Email));

        if (!await userManager.GetTwoFactorEnabledAsync(user))
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.TwoFactorAlreadyDisabled);

        var result = await userManager.SetTwoFactorEnabledAsync(user, false);

        if (!result.Succeeded)
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.Disable2FaFailed);

        await mailService.SendEmailAsync(
            new EmailMessage()
            {
                To = user.Email!,
                Subject = "Sociam 2FA Disabled",
                Message = $@"
                    2-Factor Authentication Disabled

                    Your two-factor authentication (2FA) has been successfully disabled for your Sociam account.

                    Please note that disabling 2FA may decrease the security of your account. It is recommended to keep 2FA enabled whenever possible.

                    Best regards,
                    The Sociam Team"
            });


        return Result<string>.Success(AppConstants.Disable2FaSuccess);
    }

    public async Task<Result<string>> ConfirmEmailAsync(ConfirmEmailCommand command)
    {
        var validator = new ConfirmEmailCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var user = await userManager.FindByEmailAsync(command.Email);

            if (user is null)
                return Result<string>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UnkownUser);

            var decodedAuthenticationCode = Encoding.UTF8.GetString(Convert.FromBase64String(user.Code!));

            if (decodedAuthenticationCode == command.Token)
            {
                // check if the token is expired
                if (DateTimeOffset.UtcNow > user.CodeExpiration)
                    return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.AuthCodeExpired);

                // confirm the email for the user
                user.EmailConfirmed = true;
                var identityResult = await userManager.UpdateAsync(user);

                if (!identityResult.Succeeded)
                {
                    var errors = identityResult.Errors
                        .Select(e => e.Description)
                        .ToList();

                    return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.UnableToUpdateUser, errors);
                }

                var emailMessage = new EmailMessage()
                {
                    To = command.Email,
                    Subject = "Welcome to Sociam - Email Confirmed",
                    Message = $@"
                        Congratulations, Your Email is Confirmed!

                        Hello, and welcome to Sociam. Your email address has been successfully confirmed.

                        You can now access all the features and benefits of your Sociam account.

                        If you encounter any issues or have any questions, feel free to reach out to our support team.

                        Best regards,
                        The Sociam Team"
                };


                await mailService.SendEmailAsync(emailMessage);


                await transaction.CommitAsync();
                return Result<string>.Success(AppConstants.EmailConfirmed);
            }

            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.InvalidAuthCode);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result<string>.Failure(HttpStatusCode.BadRequest, ex.Message);
        }
    }

    public async Task LogoutAsync()
    {
        contextAccessor.HttpContext?.Response.Cookies.Delete("access_token");
        contextAccessor.HttpContext?.Response.Cookies.Delete("refresh_token");
        await signInManager.SignOutAsync();
    }

    public async Task<Result<ValidateTokenResponseDto>> ValidateTokenAsync(ValidateTokenCommand command)
    {
        var validator = new ValidateTokenCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var claimsPrincipal = await tokenService.ValidateToken(command.JwtToken);

        var response = new ValidateTokenResponseDto();
        foreach (var claim in claimsPrincipal.Claims)
            response.Claims.Add(new ClaimsResponse() { ClaimType = claim.Type, ClaimValue = claim.Value });

        return Result<ValidateTokenResponseDto>.Success(response, "token is valid.");
    }

    public async Task<Result<string>> EnableMfaAsync(EnableMfaCommand command)
    {
        var validator = new EnableMfaCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<string>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UserNotExists);

        if (!await userManager.IsEmailConfirmedAsync(user))
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.EmailNotConfirmed);

        await userManager.ResetAuthenticatorKeyAsync(user);
        var authenticatorKey = await userManager.GetAuthenticatorKeyAsync(user);

        // QR code uri
        var emailEncoded = Uri.EscapeDataString(command.Email);
        var keyUri = $"otpauth://totp/syncify:{emailEncoded}?secret={authenticatorKey}&issuer=syncify&digits=6";

        // QR code
        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(keyUri, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        using var memoryStream = new MemoryStream();
        byte[] qrCodeImage = qrCode.GetGraphic(20);
        var base64QrCode = Convert.ToBase64String(qrCodeImage);

        return Result<string>.Success($"data:image/png;base64,{base64QrCode}");
    }


    public async Task<Result<SignInResponseDto>> LoginAsync(LoginUserCommand command)

    {
        var loggedInUser = await userManager.FindByEmailAsync(command.Email);

        if (loggedInUser is null)
            return Result<SignInResponseDto>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UnkownUser);

        if (!await userManager.IsEmailConfirmedAsync(loggedInUser))
            return Result<SignInResponseDto>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.EmailNotConfirmed);

        if (await userManager.GetTwoFactorEnabledAsync(loggedInUser))
        {
            //return await GetTwoFactorResponseAsync(userManager, signInManager, mailService, contextAccessor, command, loggedInUser);

            return Result<SignInResponseDto>.Failure(HttpStatusCode.Unauthorized, DomainErrors.Users.TwoFactorRequired);
        }

        // check account is locked
        if (await userManager.IsLockedOutAsync(loggedInUser))
        {
            return Result<SignInResponseDto>.Failure(
                HttpStatusCode.Unauthorized,
                $"Your account is locked until {loggedInUser.LockoutEnd!.Value.ToLocalTime()}");
        }

        var result = await signInManager.CheckPasswordSignInAsync(
            user: loggedInUser,
            password: command.Password,
            lockoutOnFailure: true);

        if (result.IsLockedOut)
        {
            var lockoutEndTime = loggedInUser.LockoutEnd!.Value
                .ConvertToUserLocalTimeZone(loggedInUser.TimeZoneId);

            return Result<SignInResponseDto>.Failure(
                 HttpStatusCode.Unauthorized, $"Your account is locked until {lockoutEndTime}");
        }

        if (!result.Succeeded)
            return Result<SignInResponseDto>.Failure(
                HttpStatusCode.Unauthorized, DomainErrors.Users.InvalidCredientials);

        var response = await CreateLoginResponseAsync(userManager, loggedInUser);
        return Result<SignInResponseDto>.Success(response);

    }

    private async Task<SignInResponseDto> CreateLoginResponseAsync(
        UserManager<ApplicationUser> manager,
        ApplicationUser loggedInUser)
    {
        var token = await tokenService.GenerateJwtTokenAsync(loggedInUser);
        var userRoles = await manager.GetRolesAsync(loggedInUser);
        var response = new SignInResponseDto()
        {
            Email = loggedInUser.Email!,
            UserName = loggedInUser.UserName!,
            IsAuthenticated = true,
            Roles = [.. userRoles],
            Token = token,
        };

        if (loggedInUser.RefreshTokens != null && loggedInUser.RefreshTokens.Any(x => x.IsActive))
        {
            var activeRefreshToken = loggedInUser.RefreshTokens.FirstOrDefault(x => x.IsActive);
            if (activeRefreshToken != null)
            {
                response.RefreshToken = activeRefreshToken.Token;
                response.RefreshTokenExpiration = activeRefreshToken.ExpiresOn.ConvertToUserLocalTimeZone(loggedInUser.TimeZoneId);
            }
        }
        else
        {
            // user not have active refresh token
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;
            response.RefreshTokenExpiration = refreshToken.ExpiresOn.ConvertToUserLocalTimeZone(loggedInUser.TimeZoneId);
            loggedInUser.RefreshTokens?.Add(refreshToken);
            await manager.UpdateAsync(loggedInUser);
        }

        var tokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddDays(_jwtSettings.ExpirationInDays)
        };

        var refreshTokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = response.RefreshTokenExpiration,
        };

        contextAccessor.HttpContext?.Response.Cookies.Append("access_token", Convert.ToBase64String(Encoding.UTF8.GetBytes(token)), tokenCookieOptions);
        contextAccessor.HttpContext?.Response.Cookies.Append("refresh_token", Convert.ToBase64String(Encoding.UTF8.GetBytes(response.RefreshToken!)), refreshTokenCookieOptions);

        return response;
    }

    public async Task<Result<SignInResponseDto>> RefreshTokenAsync(RefreshTokenCommand command)
        => (await (await (await (await CheckIfUserHasAssignedToRefreshToken(command.Token))
                        .Bind(user => SelectRefreshTokenAssignedToUser(user, command.Token))
                        .Bind(CheckIfTokenIsActive)
                        .Bind(RevokeUserTokenAndReturnsAppUser)
                        .BindAsync(GenerateNewRefreshToken))
                    .BindAsync(GenerateNewJwtToken))
                .BindAsync(CreateSignInResponse))
            .Map(authResponse => authResponse);

    public async Task<Result<bool>> RevokeTokenAsync(RevokeTokenCommand command)
        => (await (await CheckIfUserHasAssignedToRefreshToken(command.Token!))
                .Bind(appUser => SelectRefreshTokenAssignedToUser(appUser, command.Token!))
                .Bind(CheckIfTokenIsActive)
                .Bind(RevokeUserTokenAndReturnsAppUser)
                .BindAsync(UpdateApplicationUser))
            .Map(userUpdated => userUpdated);

    private async Task<Result<bool>> UpdateApplicationUser(ApplicationUser appUser)
    {
        await userManager.UpdateAsync(appUser);
        return Result<bool>.Success(true);
    }


    private async Task<Result<SignInResponseDto>> CreateSignInResponse(
        (ApplicationUser appUser, string jwtToken) appUserWithJwt)
    {
        var userRoles = await userManager.GetRolesAsync(appUserWithJwt.appUser);
        var newRefreshToken = appUserWithJwt.appUser.RefreshTokens?.FirstOrDefault(x => x.IsActive);

        var response = new SignInResponseDto
        {
            IsAuthenticated = true,
            UserName = appUserWithJwt.appUser.UserName!,
            Email = appUserWithJwt.appUser.Email!,
            Token = appUserWithJwt.jwtToken,
            Roles = [.. userRoles],
            RefreshToken = newRefreshToken?.Token,
            RefreshTokenExpiration = newRefreshToken!.ExpiresOn
        };

        return Result<SignInResponseDto>.Success(response);
    }

    private async Task<Result<(ApplicationUser appUser, string jwtToken)>>
        GenerateNewJwtToken(ApplicationUser appUser)
    {
        var jwtToken = await tokenService.GenerateJwtTokenAsync(appUser);
        return Result<(ApplicationUser appUser, string jwtToken)>.Success((appUser, jwtToken));
    }

    private async Task<Result<ApplicationUser>> GenerateNewRefreshToken(ApplicationUser appUser)
    {
        var newRefreshToken = GenerateRefreshToken();
        appUser.RefreshTokens?.Add(newRefreshToken);
        await userManager.UpdateAsync(appUser);
        return Result<ApplicationUser>.Success(appUser);
    }

    private Result<ApplicationUser> RevokeUserTokenAndReturnsAppUser(RefreshToken userRefreshToken)
    {
        userRefreshToken.RevokedOn = DateTimeOffset.UtcNow;
        var user = userManager.Users.SingleOrDefault(x =>
            x.RefreshTokens != null && x.RefreshTokens.Any(refreshToken => refreshToken.Token == userRefreshToken.Token));
        return Result<ApplicationUser>.Success(user!);
    }

    private static Result<RefreshToken> CheckIfTokenIsActive(RefreshToken userRefreshToken)
    {
        if (!userRefreshToken.IsActive)
            return Result<RefreshToken>.Failure(HttpStatusCode.BadRequest, "Inactive token");
        return Result<RefreshToken>.Success(userRefreshToken);
    }

    private static Result<RefreshToken> SelectRefreshTokenAssignedToUser(ApplicationUser user,
        string token)
    {
        var refreshToken = user.RefreshTokens?.Single(x => x.Token == token);
        if (refreshToken is not null)
            return Result<RefreshToken>.Success(refreshToken);
        return Result<RefreshToken>.Failure(HttpStatusCode.NotFound, "Token not found");
    }

    public async Task<Result<bool>> VerifyMfaAndConfirmAsync(VerifyMfaCommand command)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UserNotExists);

        var isValid = await userManager.VerifyTwoFactorTokenAsync(
            user,
            TokenOptions.DefaultAuthenticatorProvider,
            command.Code);

        if (!isValid)
            return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.Users.InvalidVerificationCode);

        user.TwoFactorEnabled = true;
        await userManager.UpdateAsync(user);

        return Result<bool>.Success(true, AppConstants.TwoFactorEnabled);

    }

    public async Task<Result<SignInResponseDto>> VerifyMfaLoginAsync(VerifyMfaLoginCommand command)
    {
        var validator = new VerifyMfaLoginCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return Result<SignInResponseDto>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UserNotExists);

        var isValid = await userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultAuthenticatorProvider, command.Code);

        if (!isValid)
            return Result<SignInResponseDto>.Failure(HttpStatusCode.Unauthorized, DomainErrors.Users.InvalidVerificationCode);

        var response = await CreateLoginResponseAsync(userManager, user);

        return Result<SignInResponseDto>.Success(response);

    }

    public Task<Result<string>> GetAccessTokenAsync(GetAccessTokenQuery query)
    {
        if (!contextAccessor.HttpContext!.Request.Cookies.ContainsKey("access_token"))
            return Task.FromResult(Result<string>.Failure(HttpStatusCode.Unauthorized, DomainErrors.Users.NoAccessTokenExists));

        if (!contextAccessor.HttpContext.Request.Cookies.TryGetValue("access_token", out var accessToken))
            return Task.FromResult(Result<string>.Failure(HttpStatusCode.Unauthorized, DomainErrors.Users.NoAccessTokenExists));

        var decodedAccessToken = Encoding.UTF8.GetString(Convert.FromBase64String(accessToken));
        return Task.FromResult(Result<string>.Success(decodedAccessToken));
    }
}
