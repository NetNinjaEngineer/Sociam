using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class CheckExistedConversationSpecification : BaseSpecification<PrivateConversation>
{
    public CheckExistedConversationSpecification(Guid conversationId) : base(c => c.Id == conversationId)
    {
        AddIncludes(c => c.SenderUser);
        AddIncludes(c => c.ReceiverUser);
        AddIncludes(c => c.Messages);
    }
}