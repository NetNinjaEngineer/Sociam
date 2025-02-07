using Asp.Versioning;
using Microsoft.AspNetCore.Http.Features;
using Sociam.Api.Extensions;
using Sociam.Api.Filters;
using Sociam.Api.WorkerServices;
using Sociam.Application.Authorization;
using System.Text.Json.Serialization;

namespace Sociam.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        ConfigureWebHostBuilder webHostBuilder)
    {
        services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddHostedService<StoryArchiveWorker>();

        services.AddSwaggerDocumentation();

        services.AddSignalR(options => options.EnableDetailedErrors = true);

        webHostBuilder.ConfigureKestrel(serverOptions =>
            serverOptions.Limits.MaxRequestBodySize = Convert.ToInt64(configuration["FormOptionsSize"]));

        services.Configure<IISServerOptions>(options =>
            options.MaxRequestBodySize = Convert.ToInt64(configuration["FormOptionsSize"]));

        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = Convert.ToInt64(configuration["FormOptionsSize"]);
            options.ValueLengthLimit = int.MaxValue;
            options.MemoryBufferThreshold = int.MaxValue;
        });

        services.AddScoped<ApiKeyFilter>();

        services.AddScoped<GuardFilter>();

        services.AddApiVersioning(options =>
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

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        services.AddOpenApi();

        services.AddAuthorization(options =>
        {
            options.AddGroupPolicies();
            options.AddStoryPolicies();
        });

        return services;
    }
}
