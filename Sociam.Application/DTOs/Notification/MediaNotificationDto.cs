using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Notification;

public sealed class MediaNotificationDto
{
    public Guid? MediaId { get; set; }
    public MediaNotificationType? MediaNotificationType { get; set; }
}