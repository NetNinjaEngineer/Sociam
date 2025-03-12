using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Replies;

namespace Sociam.Application.Features.Messages.Commands.ReplyToReplyMessage;
public sealed class ReplyToReplyMessageCommand : IRequest<Result<MessageReplyDto>>
{
    public Guid MessageId { get; set; }
    public Guid ParentReplyId { get; set; }
    public string Content { get; set; } = null!;
}