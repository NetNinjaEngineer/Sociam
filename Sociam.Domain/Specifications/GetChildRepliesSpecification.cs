using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetChildRepliesSpecification : BaseSpecification<MessageReply>
{
    public GetChildRepliesSpecification(Guid parentReplyId) : base(mr => mr.ParentReplyId == parentReplyId)
    {
        AddIncludes(mr => mr.OriginalMessage);
        AddIncludes(mr => mr.ParentReply!);
        AddIncludes(mr => mr.ChildReplies);
    }
}