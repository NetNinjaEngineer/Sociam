using Sociam.Domain.Entities.common;

namespace Sociam.Domain.Entities;
public abstract class Conversation : BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? LastMessageAt { get; set; }
}
