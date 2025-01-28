using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications
{
    public sealed class ExistedGroupConversationSpecification : BaseSpecification<GroupConversation>
    {
        public ExistedGroupConversationSpecification(Guid groupId, Guid groupConversationId)
            : base(gConversation => gConversation.GroupId == groupId &&
                    gConversation.Id == groupConversationId)
        {
            AddIncludes(gConversation => gConversation.Group);
        }
    }
}
