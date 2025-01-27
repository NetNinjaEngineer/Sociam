using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetSpecificReplyWithRelatedEntitiesSpecification : BaseSpecification<MessageReply>
{
    public GetSpecificReplyWithRelatedEntitiesSpecification(Guid replyId) : base(reply => reply.Id == replyId)
    {
        AddIncludes(reply => reply.RepliedBy);
        AddIncludes(reply => reply.OriginalMessage);
    }
}