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

app.UseSwaggerDocumentation();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseApiMiddlewares();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHubs();

app.Run();
