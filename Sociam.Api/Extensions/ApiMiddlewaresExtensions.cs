using Sociam.Api.Middleware;

namespace Sociam.Api.Extensions;

public static class ApiMiddlewaresExtensions
{
    public static IApplicationBuilder UseApiMiddlewares(this IApplicationBuilder app)
    {
        //app.UseMiddleware<MigrateDatabaseMiddleware>();
        //app.UseMiddleware<JwtValidationMiddleware>();
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        return app;
    }
}
