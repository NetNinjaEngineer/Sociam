using Sociam.Application.DTOs.Attachments;
using Sociam.Application.DTOs.Mentions;
using Sociam.Application.DTOs.Reactions;
using Sociam.Application.DTOs.Replies;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Messages;

public sealed class MessageDto
{
    public Guid Id { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderId { get; set; } = null!;
    public Guid ConversationId { get; set; }
    public MessageStatus MessageStatus { get; set; }
    public string? Content { get; set; }
    public bool IsEdited { get; set; }
    public bool IsPinned { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ReadedAt { get; set; }
    public List<AttachmentDto> Attachments { get; set; } = [];
    public List<MessageReactionDto> Reactions { get; set; } = [];
    public List<MessageMentionDto> Mentions { get; set; } = [];
    public List<MessageReplyDto> Replies { get; set; } = [];

    public static MessageDto Create(
        string senderName,
        string senderId,
        Guid conversationId,
        MessageStatus messageStatus,
        string? content,
        List<AttachmentDto>? attachments,
        DateTimeOffset? updatedAt)
    {
        return new MessageDto
        {
            SenderName = senderName,
            SenderId = senderId,
            ConversationId = conversationId,
            MessageStatus = messageStatus,
            Content = content,
            Attachments = attachments ?? [],
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = updatedAt
        };
    }

    public static MessageDto FromEntity(Message message)
    {
        var senderName = string.Concat(message.Sender.FirstName, " ", message.Sender.LastName).Trim();

        return new MessageDto
        {
            Id = message.Id,
            SenderName = senderName,
            SenderId = message.SenderId,
            ConversationId = message.ConversationId,
            MessageStatus = message.MessageStatus,
            Content = message.Content,
            IsEdited = message.IsEdited,
            IsPinned = message.IsPinned,
            CreatedAt = message.CreatedAt,
            UpdatedAt = message.UpdatedAt,
            ReadedAt = message.ReadedAt,
            Attachments = message.Attachments.Select(AttachmentDto.FromEntity).ToList() ?? [],
            Reactions = message.Reactions.Select(MessageReactionDto.FromEntity).ToList() ?? [],
            Mentions = message.Mentions.Select(MessageMentionDto.FromEntity).ToList() ?? [],
            Replies = message.Replies.Select(MessageReplyDto.FromEntity).ToList() ?? []
        };
    }
}