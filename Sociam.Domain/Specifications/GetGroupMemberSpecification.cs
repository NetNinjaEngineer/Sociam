using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications
{
    public sealed class GetGroupMemberSpecification(Guid groupId, Guid memberId) : BaseSpecification<GroupMember>(
        gm =>
        string.IsNullOrEmpty(memberId.ToString()) ||
        string.IsNullOrEmpty(groupId.ToString()) ||
        (gm.GroupId == groupId && gm.UserId == memberId.ToString()))
    {
    }
}
