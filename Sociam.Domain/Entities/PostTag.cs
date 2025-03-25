using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;

public sealed class PostTag : BaseEntity
{
    public string TaggedUserId { get; set; } = null!;
    public ApplicationUser TaggedUser { get; set; } = null!;
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
}