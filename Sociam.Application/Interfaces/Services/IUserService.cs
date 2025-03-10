using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Features.Users.Commands.ChangeAccountEmail;
using Sociam.Application.Features.Users.Commands.UpdateAvatar;
using Sociam.Application.Features.Users.Commands.UpdateCover;
using Sociam.Application.Features.Users.Commands.UpdateUserProfile;

namespace Sociam.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<ProfileDto?>> GetUserProfileAsync();
    Task<Result<ProfileDto>> UpdateUserProfileAsync(UpdateUserProfileCommand command);
    Task<Result<string>> UpdateUserAvatarAsync(UpdateAvatarCommand command);
    Task<Result<string>> UpdateUserCoverAsync(UpdateCoverCommand command);
    Task<Result<bool>> ChangeAccountEmailAsync(ChangeAccountEmailCommand command);
}