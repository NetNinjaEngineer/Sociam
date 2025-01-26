using Sociam.Domain.Entities.Identity;
using System.Security.Claims;

namespace Sociam.Application.Interfaces.Services;
public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(ApplicationUser user);
    Task<ClaimsPrincipal> ValidateToken(string token);
}
