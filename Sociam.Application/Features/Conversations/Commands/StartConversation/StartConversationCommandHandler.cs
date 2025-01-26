using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Conversations.Commands.StartConversation;
public sealed class StartConversationCommandHandler(IConversationService conversationService) :
    IRequestHandler<StartConversationCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        StartConversationCommand request, CancellationToken cancellationToken)
        => await conversationService.StartConversationAsync(request);
}
