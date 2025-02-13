using Microsoft.AspNetCore.Http;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using System.Security.Claims;

namespace Sociam.Services.Services;
public class CurrentUser(
    IHttpContextAccessor contextAccessor) : ICurrentUser
{
    public string Id => contextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.Uid)!;
    public string FullName => contextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.FullName)!;
    public string Email => contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)!;

    public string ProfilePictureUrl =>
        contextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.ProfilePictureUrl)!;

    public string TimeZoneId => contextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.TimeZoneId)!;

    public ClaimsPrincipal? GetUser() => contextAccessor.HttpContext?.User;
}
