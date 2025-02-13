using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class StoryReaction : BaseEntity
{
    public Guid StoryId { get; set; }
    public Story Story { get; set; } = null!;
    public string ReactedById { get; set; } = null!;
    public ApplicationUser ReactedBy { get; set; } = null!;
    public DateTimeOffset ReactedAt { get; set; } = DateTimeOffset.UtcNow;
    public ReactionType ReactionType { get; set; }
}