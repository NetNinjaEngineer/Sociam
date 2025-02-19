using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Notifications.Commands.MarkAsRead;
public sealed class MarkAsReadCommandHandler(INotificationService service)
    : IRequestHandler<MarkAsReadCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        => await service.MarkAsReadAsync(request);
}
