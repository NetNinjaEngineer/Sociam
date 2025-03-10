using Microsoft.AspNetCore.Identity;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Extensions;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities.Identity;
using System.Net;

namespace Sociam.Services.Services;

public sealed class UserService(ICurrentUser currentUser, UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<Result<ProfileDto?>> GetUserProfileAsync()
    {
        var profile = await userManager.GetUserProfileAsync(currentUser.Id);
        return profile == null ?
            Result<ProfileDto?>.Failure(HttpStatusCode.NotFound, "User not found") :
            Result<ProfileDto?>.Success(profile);
    }
}