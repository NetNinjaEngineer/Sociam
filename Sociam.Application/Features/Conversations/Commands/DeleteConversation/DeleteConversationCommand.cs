using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Conversations.Commands.DeleteConversation;
public sealed class DeleteConversationCommand : IRequest<Result<bool>>
{
    public required Guid ConversationId { get; set; }
}
