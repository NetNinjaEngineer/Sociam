using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;
public sealed class Story : BaseEntity
{
    public string MediaUrl { get; set; } = null!;
    public MediaType MediaType { get; set; }
    public string? Caption { get; set; }
    public List<string>? HashTags { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.Now.AddHours(24);
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<StoryView> StoryViewers { get; set; } = [];
    public ICollection<StoryReaction> StoryReactions { get; set; } = [];
    public ICollection<StoryComment> StoryComments { get; set; } = [];
}