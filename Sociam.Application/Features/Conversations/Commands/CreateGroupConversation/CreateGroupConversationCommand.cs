using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Conversations.Commands.CreateGroupConversation;
public sealed class CreateGroupConversationCommand : IRequest<Result<Guid>>
{
    public Guid GroupId { get; set; }
}
