using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using System.Security.Claims;

namespace Sociam.Services.Services;
public class CurrentUser(
    IHttpContextAccessor contextAccessor,
    IConfiguration configuration) : ICurrentUser
{
    public string Id => contextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.Uid)!;
    public string FullName => contextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.FullName)!;
    public string Email => contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)!;

    public string ProfilePictureUrl =>
        contextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.ProfilePictureUrl)!;

    public ClaimsPrincipal? GetUser() => contextAccessor.HttpContext?.User;
}
