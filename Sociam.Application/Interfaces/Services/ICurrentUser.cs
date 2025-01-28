using System.Security.Claims;

namespace Sociam.Application.Interfaces.Services;
public interface ICurrentUser
{
    string Id { get; }
    ClaimsPrincipal? GetUser();
}
