using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Features.Notifications.Commands.DeleteOne;
using Sociam.Application.Features.Notifications.Commands.MarkAsRead;
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
    Task<Result<bool>> MarkAllAsReadAsync();
    Task<Result<bool>> MarkAsReadAsync(MarkAsReadCommand command);
    Task<Result<bool>> DeleteNotificationAsync(DeleteOneCommand command);
    Task<Result<bool>> DeleteAllNotificationsAsync();
}