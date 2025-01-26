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

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseMiddleware<MigrateDatabaseMiddleware>();

app.UseMiddleware<JwtValidationMiddleware>();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHubs();

app.Run();
