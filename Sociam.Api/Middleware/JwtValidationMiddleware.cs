using Sociam.Api.Utils;
using Sociam.Application.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Sociam.Api.Middleware;

public sealed class JwtValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var authorizationHeader = context.Request.Headers.Authorization.ToString();
            var jwtToken = authorizationHeader.Replace("Bearer ", string.Empty);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadJwtToken(jwtToken);

                var ipAddressClaim = securityToken.Claims?.FirstOrDefault(c => c.Type == CustomClaims.IP)?.Value;
                var userAgentClaim = securityToken.Claims?.FirstOrDefault(c => c.Type == CustomClaims.UserAgent)?.Value;
                var fingerPrintClaim = securityToken.Claims?.FirstOrDefault(c => c.Type == CustomClaims.FingerPrint)?.Value;

                var currentIpAddress = context.Connection.RemoteIpAddress?.ToString();
                var currentUserAgent = context.Request.Headers.UserAgent.ToString();
                var currentFingerPrint = Convert.ToBase64String(
                    SHA256.HashData(Encoding.UTF8.GetBytes(string.Concat(currentIpAddress, currentUserAgent))));
                if (ipAddressClaim != currentIpAddress || userAgentClaim != currentUserAgent || fingerPrintClaim != currentFingerPrint)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsJsonAsync(
                        new GlobalErrorResponse
                        {
                            Type = "Forbidden",
                            Status = StatusCodes.Status403Forbidden,
                            Message = "Invalid Token."
                        });

                    return;
                }
            }

            await next(context);
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsJsonAsync(
                new GlobalErrorResponse
                {
                    Type = "Unauthorized",
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Unexpected behavior.",
                });
        }
    }
}
