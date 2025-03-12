using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications
{
    public sealed class ExistedGroupConversationSpecification : BaseSpecification<GroupConversation>
    {
        public ExistedGroupConversationSpecification(Guid groupConversationId)
            : base(gConversation => gConversation.Id == groupConversationId)
        {
            AddIncludes(gConversation => gConversation.Messages);
        }
    }
}
