using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class StoryNotification : Notification
{
    public Guid StoryId { get; set; }
    public string? Privacy { get; set; }

    public override string GenerateNotificationText(string senderName)
        => Type switch
        {
            NotificationType.NewStoryCreated => $"{senderName} created a new story",
            NotificationType.NewStoryComment => $"{senderName} commented on your story",
            NotificationType.NewStoryReaction => $"{senderName} reatched to your story",
            NotificationType.PrivacyChanged => $"Story privacy changed to {Privacy}",
            _ => "New story activity"
        };
}