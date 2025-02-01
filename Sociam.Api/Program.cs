using Sociam.Api;
using Sociam.Api.Extensions;
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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseApiMiddlewares();

app.UseSwaggerDocumentation();

app.MapOpenApi();

app.MapControllers();

app.UseHubs();

app.Run();