using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;

namespace Sociam.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<ProfileDto?>> GetUserProfileAsync();
}