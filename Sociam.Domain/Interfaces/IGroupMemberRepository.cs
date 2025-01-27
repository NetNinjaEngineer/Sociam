using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;

public interface IGroupMemberRepository : IGenericRepository<GroupMember>
{
    Task<bool> IsMemberInGroupAsync(Guid groupId, string memberId);
}
