using Sociam.Domain.Entities.common;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class PostMedia : BaseEntity
{
    public string Url { get; set; } = null!;
    public PostMediaType MediaType { get; set; }
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
}