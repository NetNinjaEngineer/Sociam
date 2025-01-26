using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Conversations.Queries.GetPagedConversationMessages;
public sealed class GetPagedConversationMessagesQueryHandler(
    IMessageService messageService) : IRequestHandler<GetPagedConversationMessagesQuery, Result<ConversationDto>>
{
    public async Task<Result<ConversationDto>> Handle(
        GetPagedConversationMessagesQuery request,
        CancellationToken cancellationToken) => await messageService.GetConversationMessagesAsync(request);
}
