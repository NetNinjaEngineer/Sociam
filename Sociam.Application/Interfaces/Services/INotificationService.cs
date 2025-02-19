using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Features.Notifications.Queries.GetNotification;

namespace Sociam.Application.Interfaces.Services;

public interface INotificationService
{
    Task<Result<NotificationDto>> GetNotificationAsync(GetNotificationQuery query);
}