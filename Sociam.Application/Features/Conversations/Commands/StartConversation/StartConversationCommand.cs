using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Conversations.Commands.StartConversation;
public sealed class StartConversationCommand : IRequest<Result<string>>
{
    public string ReceiverId { get; set; } = null!;
}
