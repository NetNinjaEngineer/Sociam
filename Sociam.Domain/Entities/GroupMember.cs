using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class GroupMember : BaseEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;
    public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.Now;
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public GroupMemberRole Role { get; set; }
    public string AddedById { get; set; } = null!;
    public ApplicationUser AddedBy { get; set; } = null!;
}