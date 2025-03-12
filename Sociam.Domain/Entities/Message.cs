using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class Message : BaseEntity
{
    public Guid ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;
    public string? Content { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ReadedAt { get; set; }
    public MessageStatus MessageStatus { get; set; }
    public bool IsEdited { get; set; }
    public bool IsPinned { get; set; }
    public ICollection<Attachment> Attachments { get; set; } = [];
    public ICollection<MessageReaction> Reactions { get; set; } = [];
    public ICollection<MessageMention> Mentions { get; set; } = [];
    public ICollection<MessageReply> Replies { get; set; } = [];
    public string SenderId { get; set; } = null!;
    public ApplicationUser Sender { get; set; } = null!;
}