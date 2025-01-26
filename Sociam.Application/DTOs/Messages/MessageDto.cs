using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Messages;

public sealed class MessageDto
{
    public string SenderName { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public MessageStatus MessageStatus { get; set; }
    public string? Content { get; set; }
    public List<AttachmentDto>? Attachments { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

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
}