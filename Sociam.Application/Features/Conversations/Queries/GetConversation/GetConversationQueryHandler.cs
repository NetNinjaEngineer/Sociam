using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Conversations.Queries.GetConversation;
public sealed class GetConversationQueryHandler(
    IConversationService conversationService) : IRequestHandler<GetConversationQuery, Result<ConversationDto>>
{
    public async Task<Result<ConversationDto>> Handle(GetConversationQuery request,
        CancellationToken cancellationToken)
        => await conversationService.GetConversationBetweenAsync(request);
}
