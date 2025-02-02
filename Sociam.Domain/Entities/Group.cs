using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;
public sealed class Group : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public GroupPrivacy GroupPrivacy { get; set; }
    public string CreatedByUserId { get; set; } = null!;
    public ApplicationUser CreatedByUser { get; set; } = null!;
    public string? PictureName { get; set; }
    public ICollection<GroupConversation> GroupConversations { get; set; } = [];
    public ICollection<GroupMember> Members { get; set; } = [];
    public ICollection<JoinGroupRequest> JoinGroupRequests { get; set; } = [];
}