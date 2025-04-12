using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;

public sealed class PostComment : BaseEntity
{
    public string Comment { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public string CreatedById { get; set; } = null!;
    public ApplicationUser CreatedBy { get; set; } = null!;
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
    public Guid? ParentCommentId { get; set; }
    public PostComment? ParentComment { get; set; }
    public ICollection<PostComment> Replies { get; set; } = [];
}