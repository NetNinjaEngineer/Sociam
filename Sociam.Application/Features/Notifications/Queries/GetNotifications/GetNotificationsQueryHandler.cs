using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Notifications.Queries.GetNotifications;
public sealed class GetNotificationsQueryHandler(INotificationService service) :
    IRequestHandler<GetNotificationsQuery, Result<PagedResult<NotificationDto>>>
{
    public async Task<Result<PagedResult<NotificationDto>>> Handle(GetNotificationsQuery request,
        CancellationToken cancellationToken)
        => await service.GetNotificationsAsync(request.NotificationsSpecParams);
}
