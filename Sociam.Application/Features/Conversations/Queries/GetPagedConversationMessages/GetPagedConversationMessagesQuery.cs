using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;

namespace Sociam.Application.Features.Conversations.Queries.GetPagedConversationMessages;
public sealed class GetPagedConversationMessagesQuery : IRequest<Result<ConversationDto>>
{
    public Guid ConversationId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
