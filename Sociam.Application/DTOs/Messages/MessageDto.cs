using Sociam.Application.DTOs.Attachments;
using Sociam.Application.DTOs.Mentions;
using Sociam.Application.DTOs.Reactions;
using Sociam.Application.DTOs.Replies;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Messages;

// There is an issue with sender and receiver of the message related to database modeling
public sealed class MessageDto
{
    public string SenderName { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public Guid? PrivateConversationId { get; set; }
    public Guid? GroupConversationId { get; set; }
    public MessageStatus MessageStatus { get; set; }
    public string? Content { get; set; }
    public bool IsEdited { get; set; }
    public bool IsPinned { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ReadedAt { get; set; }
    public List<AttachmentDto>? Attachments { get; set; } = [];
    public List<MessageReactionDto>? Reactions { get; set; } = [];
    public List<MessageMentionDto>? Mentions { get; set; } = [];
    public List<MessageReplyDto>? Replies { get; set; } = [];

    public static MessageDto Create(string senderName,
        string receiverName,
        MessageStatus messageStatus,
        string? content,
        List<AttachmentDto>? attachments,
        DateTimeOffset createdAt,
        DateTimeOffset? updatedAt)
        => new()
        {
            SenderName = senderName,
            ReceiverName = receiverName,
            MessageStatus = messageStatus,
            Content = content,
            Attachments = attachments,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };

    public static MessageDto FromEntity(Message message)
    {
        return new()
        {
            SenderName = string.Concat(message.Sender?.FirstName, " ", message.Sender?.LastName),
            ReceiverName = string.Concat(message.Receiver?.FirstName, " ", message.Receiver?.LastName),
            PrivateConversationId = message.PrivateConversationId,
            GroupConversationId = message.GroupConversationId,
            MessageStatus = message.MessageStatus,
            Content = message.Content,
            IsEdited = message.IsEdited,
            IsPinned = message.IsPinned,
            CreatedAt = message.CreatedAt,
            UpdatedAt = message.UpdatedAt,
            ReadedAt = message.ReadedAt,
            Attachments = [.. message.Attachments.Select(AttachmentDto.FromEntity)],
            Reactions = [.. message.Reactions.Select(MessageReactionDto.FromEntity)],
            Mentions = [.. message.Mentions.Select(MessageMentionDto.FromEntity)],
            Replies = [.. message.Replies.Select(MessageReplyDto.FromEntity)]
        };
    }
}