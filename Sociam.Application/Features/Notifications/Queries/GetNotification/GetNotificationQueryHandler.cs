using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Notifications.Queries.GetNotification;
public sealed class GetNotificationQueryHandler(INotificationService service) :
    IRequestHandler<GetNotificationQuery, Result<NotificationDto>>
{
    public async Task<Result<NotificationDto>> Handle(
        GetNotificationQuery request, CancellationToken cancellationToken)
        => await service.GetNotificationAsync(request);
}
