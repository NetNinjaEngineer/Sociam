using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories;

public sealed class GroupMemberRepository(ApplicationDbContext context)
    : GenericRepository<GroupMember>(context), IGroupMemberRepository
{
    public async Task<bool> IsAdminInGroupAsync(Guid groupId, string memberId)
        => await context.GroupMembers.AnyAsync(
            member => member.GroupId == groupId && member.UserId == memberId && member.Role == GroupMemberRole.Admin);

    public async Task<bool> IsMemberInGroupAsync(Guid groupId, string memberId)
        => await context.GroupMembers.AnyAsync(
            member => member.GroupId == groupId && member.UserId == memberId);
}
