using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Notification;

public sealed class NotificationDto
{
    public string RecipientId { get; set; } = null!;
    public string SenderId { get; set; } = null!;
    public NotificationType Type { get; set; }
    public NotificationStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ReadAt { get; set; }
    public string Message { get; set; } = null!;
    public string? ActionUrl { get; set; }
    public string SenderName { get; set; } = null!;
    public string RecipientName { get; set; } = null!;
    public Guid? PostId { get; set; }
    public string? PostContent { get; set; }
    public Guid? StoryId { get; set; }
    public string? Privacy { get; set; }
    public Guid? MediaId { get; set; }
    public MediaNotificationType? MediaNotificationType { get; set; }
    public string? GroupId { get; set; }
    public string? GroupName { get; set; }
    public string? GroupRole { get; set; }
    public string? NKind { get; set; }
}