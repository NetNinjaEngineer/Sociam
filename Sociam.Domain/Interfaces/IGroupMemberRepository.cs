using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;

public interface IGroupMemberRepository : IGenericRepository<GroupMember>
{
    Task<bool> IsAdminInGroupAsync(Guid groupId, string memberId);
    Task<bool> IsMemberInGroupAsync(Guid groupId, string memberId);
}
