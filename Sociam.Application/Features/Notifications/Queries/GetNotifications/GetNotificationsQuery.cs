using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Application.Features.Notifications.Queries.GetNotifications;
public sealed class GetNotificationsQuery : IRequest<Result<PagedResult<NotificationDto>>>
{
    public NotificationsSpecParams? NotificationsSpecParams { get; set; }
}
