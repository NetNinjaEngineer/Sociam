using Sociam.Domain.Entities.common;

namespace Sociam.Domain.Entities;
public class Conversation : BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? LastMessageAt { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
}
