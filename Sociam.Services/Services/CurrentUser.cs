using Microsoft.AspNetCore.Http;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using System.Security.Claims;

namespace Sociam.Services.Services;
public class CurrentUser(IHttpContextAccessor contextAccessor) : ICurrentUser
{
    public string Id => contextAccessor.HttpContext!.User.FindFirstValue(CustomClaims.Uid)!;

    public ClaimsPrincipal? GetUser() => contextAccessor.HttpContext?.User;
}
