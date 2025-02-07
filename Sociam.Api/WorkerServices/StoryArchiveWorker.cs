using Microsoft.EntityFrameworkCore;
using Sociam.Infrastructure.Persistence;

namespace Sociam.Api.WorkerServices;

public sealed class StoryArchiveWorker(
    IServiceScopeFactory serviceScopeFactory,
    IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var databaseContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var expiredStories = await databaseContext.Stories
                .AsNoTracking()
                .Where(s => s.ExpiresAt <= DateTimeOffset.Now && !s.IsArchived)
                .ToListAsync(cancellationToken: stoppingToken);

            if (expiredStories.Count > 0)
                await databaseContext.Stories.ExecuteUpdateAsync(
                    x => x.SetProperty(story => story.IsArchived, true), stoppingToken);

            await Task.Delay(TimeSpan.FromDays(Convert.ToInt32(configuration["StoryExpirationCheckIntervalDays"])), stoppingToken);
        }
    }
}