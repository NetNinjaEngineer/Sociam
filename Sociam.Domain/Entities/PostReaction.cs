using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class PostReaction : BaseEntity
{
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
    public DateTimeOffset ReactedAt { get; set; } = DateTimeOffset.UtcNow;
    public ReactionType Type { get; set; }
    public string ReactedById { get; set; } = null!;
    public ApplicationUser ReactedBy { get; set; } = null!;
}