using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Replies;

namespace Sociam.Application.Features.Messages.Commands.ReplyToMessage;

public sealed class ReplyToMessageCommand : IRequest<Result<MessageReplyDto>>
{
    public Guid MessageId { get; set; }
    public string Content { get; set; } = null!;
}
