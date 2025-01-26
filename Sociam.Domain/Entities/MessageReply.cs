using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class MessageReply : BaseEntity
{
    public Guid OriginalMessageId { get; set; }
    public Message OriginalMessage { get; set; } = null!;
    public bool IsEdited { get; set; }
    public DateTimeOffset? EditedAt { get; set; }
    public string Content { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public string RepliedById { get; set; } = null!;
    public ApplicationUser RepliedBy { get; set; } = null!;
    public ReplyStatus ReplyStatus { get; set; } = ReplyStatus.Active;
    public Guid? ParentReplyId { get; set; }
    public MessageReply? ParentReply { get; set; }
    public ICollection<MessageReply> ChildReplies { get; set; } = [];
}