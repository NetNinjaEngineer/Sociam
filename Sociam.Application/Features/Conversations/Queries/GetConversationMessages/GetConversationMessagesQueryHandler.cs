using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Conversations.Queries.GetConversationMessages;
public sealed class GetConversationMessagesQueryHandler(IMessageService messageService)
    : IRequestHandler<GetConversationMessagesQuery, Result<ConversationDto>>
{
    public async Task<Result<ConversationDto>> Handle(GetConversationMessagesQuery request, CancellationToken cancellationToken)
        => await messageService.GetConversationMessagesAsync(request.ConversationId);
}
