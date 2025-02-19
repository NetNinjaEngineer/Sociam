using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<long> GetUnReadNotificationsCountAsync(string currentUserId);
    Task<long> GetReadNotificationsCountAsync(string currentUserId);
    Task<bool> MarkAllAsReadAsync(string currentUserId);
    Task<bool> MarkAsReadAsync(string currentUserId, Guid notificationId);
    Task<bool> DeleteOneAsync(string currentUserId, Guid notificationId);
    Task<bool> DeleteAllAsync(string currentUserId);
}