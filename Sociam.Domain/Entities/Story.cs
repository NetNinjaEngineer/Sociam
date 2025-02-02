using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;
public abstract class Story : BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.Now.AddHours(24);
    public StoryPrivacy StoryPrivacy { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<StoryView> StoryViewers { get; set; } = [];
    public ICollection<StoryReaction> StoryReactions { get; set; } = [];
    public ICollection<StoryComment> StoryComments { get; set; } = [];
}