using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Features.Notifications.Queries.GetNotification;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Application.Interfaces.Services;

public interface INotificationService
{
    Task<Result<NotificationDto>> GetNotificationAsync(GetNotificationQuery query);
    Task<Result<PagedResult<NotificationDto>>> GetNotificationsAsync(NotificationsSpecParams? @params);
    Task<Result<long>> GetUnReadNotificationsCountAsync();
    Task<Result<long>> GetReadNotificationsCountAsync();
}