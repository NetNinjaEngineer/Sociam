using Asp.Versioning;
using Microsoft.AspNetCore.Http.Features;
using Scalar.AspNetCore;
using Sociam.Api.Extensions;
using Sociam.Api.Filters;
using Sociam.Api.WorkerServices;
using Sociam.Application;
using Sociam.Application.Authorization.Helpers;
using Sociam.Infrastructure;
using Sociam.Persistence;
using Sociam.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructureDependencies(builder.Configuration)
    .AddServicesDependencies(builder.Configuration)
    .AddApplicationDependencies(builder.Configuration)
    .AddPersistenceDependecies(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddHostedService<StoryArchiveWorker>();

builder.Services.AddSignalR(options => options.EnableDetailedErrors = true);

builder.WebHost.ConfigureKestrel(serverOptions =>
    serverOptions.Limits.MaxRequestBodySize = Convert.ToInt64(builder.Configuration["FormOptionsSize"]));

builder.Services.Configure<IISServerOptions>(options =>
    options.MaxRequestBodySize = Convert.ToInt64(builder.Configuration["FormOptionsSize"]));

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = Convert.ToInt64(builder.Configuration["FormOptionsSize"]);
    options.ValueLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddScoped<ApiKeyFilter>();

builder.Services.AddScoped<GuardFilter>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new QueryStringApiVersionReader("version"));
})
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddGroupPolicies();
    options.AddStoryPolicies();
});

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseSwaggerDocumentation();

app.MapScalarApiReference(options =>
{
    options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    options.WithApiKeyAuthentication(c => c.Token = builder.Configuration.GetValue<string>("ApiKey"));
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseApiMiddlewares();

app.MapControllers();

app.UseHubs();

app.Run();