using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;

namespace Sociam.Application.Features.Notifications.Queries.GetNotifications;
public sealed class GetNotificationsQuery : IRequest<Result<IReadOnlyList<NotificationDto>>>
{
}
