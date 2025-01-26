using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Conversations.Commands.CreateGroupConversation;
public sealed class CreateGroupConversationCommandHandler(IConversationService conversationService)
    : IRequestHandler<CreateGroupConversationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateGroupConversationCommand request, CancellationToken cancellationToken)
        => await conversationService.CreateGroupConversationAsync(request);
}
