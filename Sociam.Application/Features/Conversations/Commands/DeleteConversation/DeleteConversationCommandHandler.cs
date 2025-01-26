using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Conversations.Commands.DeleteConversation;
public sealed class DeleteConversationCommandHandler(
    IConversationService conversationService) : IRequestHandler<DeleteConversationCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteConversationCommand request,
        CancellationToken cancellationToken)
        => await conversationService.DeleteConversationAsync(request);
}
