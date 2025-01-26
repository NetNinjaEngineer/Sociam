using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Commands.MarkMessageAsRead;
public sealed class MarkMessageAsReadCommandHandler(IMessageService messageService) : IRequestHandler<MarkMessageAsReadCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(MarkMessageAsReadCommand request,
        CancellationToken cancellationToken) => await messageService.MarkMessageAsReadAsync(request);
}
