using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Notifications.Commands.MarkAllAsRead;
public sealed class MarkAllAsReadCommandHandler(INotificationService service)
    : IRequestHandler<MarkAllAsReadCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(MarkAllAsReadCommand request, CancellationToken cancellationToken)
        => await service.MarkAllAsReadAsync();
}
