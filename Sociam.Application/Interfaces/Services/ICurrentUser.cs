using System.Security.Claims;

namespace Sociam.Application.Interfaces.Services;
public interface ICurrentUser
{
    string Id { get; }
    string FullName { get; }
    string Email { get; }
    string ProfilePictureUrl { get; }
    string TimeZoneId { get; }
    ClaimsPrincipal? GetUser();
}
