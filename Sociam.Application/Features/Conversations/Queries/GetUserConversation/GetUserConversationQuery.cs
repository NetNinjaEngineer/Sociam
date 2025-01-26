using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;

namespace Sociam.Application.Features.Conversations.Queries.GetUserConversation;
public sealed class GetUserConversationQuery : IRequest<Result<ConversationDto>>
{
    public string UserId { get; set; } = null!;
}
