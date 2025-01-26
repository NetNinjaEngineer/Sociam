using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class Message : BaseEntity
{
    public Guid? PrivateConversationId { get; set; }
    public PrivateConversation? PrivateConversation { get; set; }
    public Guid? GroupConversationId { get; set; }
    public GroupConversation? GroupConversation { get; set; }
    public string? Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public MessageStatus MessageStatus { get; set; }
    public ICollection<Attachment> Attachments { get; set; } = [];
}