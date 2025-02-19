using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Notifications.Commands.DeleteAll;
public sealed class DeleteAllCommandHandler(INotificationService service)
    : IRequestHandler<DeleteAllCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteAllCommand request, CancellationToken cancellationToken)
        => await service.DeleteAllNotificationsAsync();
}
