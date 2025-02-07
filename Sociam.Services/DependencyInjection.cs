using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities.Identity;
using Sociam.Infrastructure.Persistence;
using Sociam.Services.Services;
using System.Text;

namespace Sociam.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            var duration = Convert.ToDouble(configuration["DefaultLockoutMinutes"]);
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(duration);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;

            options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IFriendshipService, FriendshipService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddMemoryCache();

        var jwtSettings = new JwtSettings();
        configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        services.Configure<AuthOptions>(configuration.GetSection("Authentication"));

        var authOptions = new AuthOptions();
        configuration.GetSection("Authentication").Bind(authOptions);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                            context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            });
        //.AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
        //{
        //    options.AppId = authOptions.FacebookOptions.AppId;
        //    options.AppSecret = authOptions.FacebookOptions.AppSecret;
        //})
        //.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
        //{
        //    options.ClientId = authOptions.GoogleOptions.ClientId;
        //    options.ClientSecret = authOptions.GoogleOptions.ClientSecret;
        //});

        services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));

        services.AddTransient<IMailService, MailService>();

        services.AddScoped<IFollowingService, FollowingService>();

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IFileService, FileService>();

        services.AddScoped<IStoryService, StoryService>();

        services.AddScoped<IMessageService, MessageService>();

        services.AddScoped<IConversationService, ConversationService>();

        services.AddScoped<IGroupService, GroupService>();

        services.AddScoped<INotificationUrlGenerator, NotificationUrlGenerator>();

        return services;
    }
}
