using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;
public sealed class StoryView : BaseEntity
{
    public Guid? StoryId { get; set; }
    public Story? Story { get; set; }
    public string ViewerId { get; set; } = null!;
    public ApplicationUser Viewer { get; set; } = null!;
    public bool IsViewed { get; set; }
    public DateTimeOffset? ViewedAt { get; set; }
}
