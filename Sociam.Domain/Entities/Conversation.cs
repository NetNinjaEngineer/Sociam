namespace Sociam.Domain.Entities;
public abstract class Conversation : BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? LastMessageAt { get; set; }
}
