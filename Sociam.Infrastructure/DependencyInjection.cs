using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sociam.Domain.Interfaces;
using Sociam.Infrastructure.Persistence;
using Sociam.Infrastructure.Persistence.Repositories;

namespace Sociam.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IFriendshipRepository, FriendshipRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<IPrivateConversationRepository, PrivateConversationRepository>();

        return services;
    }
}
