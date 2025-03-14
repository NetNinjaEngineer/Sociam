using Microsoft.EntityFrameworkCore;
using Sociam.Infrastructure.Persistence;

namespace Sociam.Api.Middleware;

public sealed class MigrateDatabaseMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var scopeFactory = context.RequestServices.GetRequiredService<IServiceScopeFactory>();

        var scope = scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();

        await dbContext.SeedDatabaseAsync();

        await next(context);
    }
}
