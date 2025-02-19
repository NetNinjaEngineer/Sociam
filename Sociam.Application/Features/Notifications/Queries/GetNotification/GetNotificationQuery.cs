using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;

namespace Sociam.Application.Features.Notifications.Queries.GetNotification;
public sealed class GetNotificationQuery : IRequest<Result<NotificationDto>>
{
    public Guid Id { get; set; }
}
