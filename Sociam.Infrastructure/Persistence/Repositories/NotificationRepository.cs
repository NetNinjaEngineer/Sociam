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
}