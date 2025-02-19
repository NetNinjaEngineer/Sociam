using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories;

public sealed class NotificationRepository(ApplicationDbContext context) :
    GenericRepository<Notification>(context), INotificationRepository
{
    public async Task<long> GetUnReadNotificationsCountAsync(string currentUserId)
        => await context.Notifications
            .AsNoTracking()
            .LongCountAsync(notification =>
                notification.Status == NotificationStatus.UnRead && notification.RecipientId == currentUserId);

    public async Task<long> GetReadNotificationsCountAsync(string currentUserId)
        => await context.Notifications
            .AsNoTracking()
            .LongCountAsync(notification =>
                notification.Status == NotificationStatus.Read && notification.RecipientId == currentUserId);

    public async Task<bool> MarkAllAsReadAsync(string currentUserId)
    {
        await context.Notifications
            .Where(notification =>
                notification.Status == NotificationStatus.UnRead &&
                notification.RecipientId == currentUserId)
            .ExecuteUpdateAsync(x => x
                .SetProperty(notification => notification.Status, NotificationStatus.Read)
                .SetProperty(notification => notification.ReadAt, DateTimeOffset.UtcNow));

        return true;
    }

    public async Task<bool> MarkAsReadAsync(string currentUserId, Guid notificationId)
    {
        await context.Notifications
            .Where(
                notification =>
                    notification.RecipientId == currentUserId &&
                    notification.Status == NotificationStatus.UnRead &&
                    notification.Id == notificationId)
            .ExecuteUpdateAsync(
                x => x
                    .SetProperty(notification => notification.Status, NotificationStatus.Read)
                    .SetProperty(notification => notification.ReadAt, DateTimeOffset.UtcNow));

        return true;
    }

    public async Task<bool> DeleteOneAsync(string currentUserId, Guid notificationId)
    {
        await context.Notifications
            .Where(notification => notification.Id == notificationId && notification.RecipientId == currentUserId)
            .ExecuteDeleteAsync();

        return true;
    }

    public async Task<bool> DeleteAllAsync(string currentUserId)
    {
        await context.Notifications
            .Where(notification => notification.RecipientId == currentUserId)
            .ExecuteDeleteAsync();

        return true;
    }
}