using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;
public sealed class StoryView : BaseEntity
{
    public Guid? StoryId { get; set; }
    public Story? Story { get; set; }
    public string ViewerId { get; set; } = null!;
    public ApplicationUser Viewer { get; set; } = null!;
    public DateTimeOffset ViewedAt { get; set; } = DateTimeOffset.Now;
}
