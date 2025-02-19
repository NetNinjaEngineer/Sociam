using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<long> GetUnReadNotificationsCountAsync(string currentUserId);
    Task<long> GetReadNotificationsCountAsync(string currentUserId);
}