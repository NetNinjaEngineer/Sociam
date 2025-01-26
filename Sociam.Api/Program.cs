using Sociam.Api;
using Sociam.Api.Extensions;
using Sociam.Api.Middleware;
using Sociam.Application;
using Sociam.Infrastructure;
using Sociam.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructureDependencies(builder.Configuration)
    .AddServicesDependencies(builder.Configuration)
    .AddApplicationDependencies(builder.Configuration)
    .AddApiDependencies(builder.Configuration, builder.WebHost);

var app = builder.Build();

app.UseSwaggerDocumentation();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseMiddleware<JwtValidationMiddleware>();

app.UseMiddleware<MigrateDatabaseMiddleware>();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHubs();

app.Run();
