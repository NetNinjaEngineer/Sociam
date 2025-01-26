using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;

namespace Sociam.Application.Features.Conversations.Queries.GetConversation;
public sealed class GetConversationQuery : IRequest<Result<ConversationDto>>
{
    public required string SenderId { get; set; }
    public required string ReceiverId { get; set; }
}
