using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;

public sealed class StoryComment : BaseEntity
{
    public Guid StoryId { get; set; }
    public Story Story { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public string CommentedById { get; set; } = null!;
    public ApplicationUser CommentedBy { get; set; } = null!;
    public DateTimeOffset CommentedAt { get; set; } = DateTimeOffset.UtcNow;
}