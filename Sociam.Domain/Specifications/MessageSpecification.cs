using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;
public sealed class MessageSpecification : BaseSpecification<Message>
{
    public MessageSpecification()
    {
        AddIncludes(m => m.Attachments);
        AddIncludes(m => m.Conversation);
    }
}
