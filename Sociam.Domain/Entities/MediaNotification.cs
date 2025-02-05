using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class MediaNotification : Notification
{
    public Guid MediaId { get; set; }
    public MediaNotificationType MediaNotificationType { get; set; }


    public override string GenerateNotificationText(string senderName)
        => Type switch
        {
            NotificationType.NewMedia => $"{senderName} added a new {MediaNotificationType.ToString().ToLowerInvariant()}",
            NotificationType.MediaTag => $"{senderName} tagged you in a {MediaNotificationType.ToString().ToLowerInvariant()}",
            NotificationType.MediaComment => $"{senderName} commented on your {MediaNotificationType.ToString().ToLowerInvariant()}",
            NotificationType.MediaReaction => $"{senderName} reatched to your {MediaNotificationType.ToString().ToLowerInvariant()}",
            _ => "New media activity"
        };
}