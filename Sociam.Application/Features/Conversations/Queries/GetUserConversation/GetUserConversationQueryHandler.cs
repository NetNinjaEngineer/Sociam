using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Conversations.Queries.GetUserConversation;
public sealed class GetUserConversationQueryHandler(
    IMessageService messageService) : IRequestHandler<GetUserConversationQuery, Result<ConversationDto>>
{
    public async Task<Result<ConversationDto>> Handle(GetUserConversationQuery request,
        CancellationToken cancellationToken)
        => await messageService.GetUserConversationsAsync(request);
}
