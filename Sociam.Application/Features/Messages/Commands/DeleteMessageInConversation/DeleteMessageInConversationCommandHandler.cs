using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Commands.DeleteMessageInConversation;
public sealed class DeleteMessageInConversationCommandHandler(
    IMessageService messageService) : IRequestHandler<DeleteMessageInConversationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteMessageInConversationCommand request,
        CancellationToken cancellationToken)
        => await messageService.DeleteMessageInConversationAsync(request);
}
