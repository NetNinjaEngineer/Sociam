using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sociam.Application.Authorization.Handlers;
using Sociam.Application.Helpers;

namespace Sociam.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(
    this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(assembly: Assembly.GetExecutingAssembly()));

        services.Configure<JwtSettings>(jwtOptions =>
        {
            var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
            jwtOptions.Key = jwtSettingsSection["Key"]!;
            jwtOptions.Issuer = jwtSettingsSection["Issuer"]!;
            jwtOptions.Audience = jwtSettingsSection["Audience"]!;
            jwtOptions.ExpirationInDays = Convert.ToInt32(jwtSettingsSection["ExpirationInDays"]);
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped(typeof(DataShaper<>));

        services.AddScoped<IAuthorizationHandler, GroupOperationAuthorizationHandler>();
        services.AddScoped<IAuthorizationHandler, StoryOperationAuthorizationHandler>();
        services.AddScoped<IAuthorizationHandler, PostOperationsAuthorizationHandler>();

        return services;
    }
}
