using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Notifications.Commands.DeleteOne;
public sealed class DeleteOneCommandHandler(INotificationService service)
    : IRequestHandler<DeleteOneCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteOneCommand request, CancellationToken cancellationToken)
        => await service.DeleteNotificationAsync(request);
}
