using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;
public sealed class GetExistedMessageSpecification : BaseSpecification<Message>
{
    public GetExistedMessageSpecification(Guid messageId) : base(m => m.Id == messageId)
    {
        AddIncludes(m => m.Attachments);
        AddIncludes(m => m.PrivateConversation!);
        AddOrderByDescending(m => m.CreatedAt);
        DisableTracking();
    }

    public GetExistedMessageSpecification(Guid messageId, Guid conversationId) : base(m => m.Id == messageId && m.PrivateConversationId == conversationId)
    {
        AddIncludes(m => m.Attachments);
    }
}
