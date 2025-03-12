using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Replies;

namespace Sociam.Application.Features.Messages.Queries.LoadSubReplies;
public sealed class LoadSubRepliesQuery : IRequest<Result<IReadOnlyList<MessageReplyDto>>>
{
    public Guid MessageId { get; set; }
    public Guid ParentReplyId { get; set; }
}
