using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Notifications.Queries.GetNotifications;
public sealed class GetNotificationsQueryHandler(INotificationService service) :
    IRequestHandler<GetNotificationsQuery, Result<IReadOnlyList<NotificationDto>>>
{
    public async Task<Result<IReadOnlyList<NotificationDto>>> Handle(GetNotificationsQuery request,
        CancellationToken cancellationToken)
        => await service.GetNotificationsAsync();
}
