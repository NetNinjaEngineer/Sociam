using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Extensions;
using Sociam.Application.Features.Users.Commands.UpdateUserProfile;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities.Identity;
using System.Net;

namespace Sociam.Services.Services;

public sealed class UserService(
    IMapper mapper,
    ICurrentUser currentUser,
    UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<Result<ProfileDto?>> GetUserProfileAsync()
    {
        var profile = await userManager.GetUserProfileAsync(currentUser.Id);
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
}