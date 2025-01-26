using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;

namespace Sociam.Application.Features.Conversations.Queries.GetConversationMessages;
public sealed class GetConversationMessagesQuery : IRequest<Result<ConversationDto>>
{
    public Guid ConversationId { get; set; }
}
