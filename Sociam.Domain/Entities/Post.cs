using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;
public sealed class Post : BaseEntity
{
    public string? Text { get; set; }
    public string CreatedById { get; set; } = null!;
    public long SharesCount { get; set; }
    public ApplicationUser CreatedBy { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public PostPrivacy Privacy { get; set; }
    public Guid? OriginalPostId { get; set; }
    public Post? OriginalPost { get; set; }
    public PostLocation? Location { get; set; }
    public PostFeeling Feeling { get; set; } = PostFeeling.None;
    public ICollection<PostTag> Tags { get; set; } = [];
    public ICollection<PostReaction> Reactions { get; set; } = [];
    public ICollection<PostComment> Comments { get; set; } = [];
    public ICollection<PostMedia> Media { get; set; } = [];
}
