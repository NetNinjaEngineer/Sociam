using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Features.Users.Commands.UpdateUserProfile;

namespace Sociam.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<ProfileDto?>> GetUserProfileAsync();
    Task<Result<ProfileDto>> UpdateUserProfileAsync(UpdateUserProfileCommand command);
}