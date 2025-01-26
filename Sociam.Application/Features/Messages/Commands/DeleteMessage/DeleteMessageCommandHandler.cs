using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Commands.DeleteMessage;
public sealed class DeleteMessageCommandHandler(
    IMessageService messageService) : IRequestHandler<DeleteMessageCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        DeleteMessageCommand request,
        CancellationToken cancellationToken)
        => await messageService.DeleteMessageAsync(request.MessageId);
}
