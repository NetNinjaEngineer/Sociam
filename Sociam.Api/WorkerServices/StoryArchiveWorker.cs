using Microsoft.EntityFrameworkCore;
using Sociam.Application.Interfaces.Services;
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
            var currentUser = scope.ServiceProvider.GetRequiredService<ICurrentUser>();

            if (!string.IsNullOrEmpty(currentUser.TimeZoneId))
            {
                // Get the user's time zone
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(currentUser.TimeZoneId);

                // Calculate the current time in UTC
                var currentTimeUtc = DateTimeOffset.UtcNow;

                // Calculate the UTC offset for the user's time zone
                var utcOffset = timeZone.GetUtcOffset(currentTimeUtc.DateTime);

                var expiredStories = await databaseContext.Stories
                    .AsNoTracking()
                    .Where(s => s.ExpiresAt <= currentTimeUtc + utcOffset && !s.IsArchived)
                    .ToListAsync(cancellationToken: stoppingToken);

                if (expiredStories.Count > 0)
                    await databaseContext.Stories.ExecuteUpdateAsync(
                        x => x.SetProperty(story => story.IsArchived, true), stoppingToken);

            }

            await Task.Delay(TimeSpan.FromDays(Convert.ToInt32(configuration["StoryExpirationCheckIntervalDays"])), stoppingToken);
        }
    }
}