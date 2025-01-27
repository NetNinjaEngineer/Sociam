using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Replies;

public sealed class MessageReplyDto
{
    public Guid ReplyId { get; set; }
    public Guid OriginalMessageId { get; set; }
    public bool IsEdited { get; set; }
    public DateTimeOffset? EditedAt { get; set; }
    public string Content { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public string RepliedById { get; set; } = null!;
    public string RepliedBy { get; set; } = null!;
    public ReplyStatus ReplyStatus { get; set; }
    public Guid? ParentReplyId { get; set; }

    public static MessageReplyDto FromEntity(MessageReply messageReply)
        => new()
        {
            ReplyId = messageReply.Id,
            OriginalMessageId = messageReply.OriginalMessageId,
            IsEdited = messageReply.IsEdited,
            EditedAt = messageReply.EditedAt,
            Content = messageReply.Content,
            CreatedAt = messageReply.CreatedAt,
            RepliedById = messageReply.RepliedById,
            RepliedBy = string.Concat(
                messageReply.RepliedBy?.FirstName, " ", messageReply.RepliedBy?.LastName),
            ReplyStatus = messageReply.ReplyStatus,
            ParentReplyId = messageReply.ParentReplyId
        };

}
