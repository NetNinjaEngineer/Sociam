using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Services.Services;

public sealed class NotificationUrlGenerator : INotificationUrlGenerator
{
    public string GenerateUrl(Notification notification)
    {
        return notification switch
        {
            MediaNotification mediaNotification => GenerateMediaNotificationUrl(mediaNotification),
            StoryNotification storyNotification => GenerateStoryNotificationUrl(storyNotification),
            GroupNotification groupNotification => GenerateGroupNotificationUrl(groupNotification),
            NetworkNotification networkNotification => GenerateNetworkNotificationUrl(networkNotification),
            PostNotification postNotification => GeneratePostNotificationUrl(postNotification),
            _ => throw new ArgumentException("Ïnvalid Notification Type")
        };
    }

    private static string GenerateMediaNotificationUrl(MediaNotification notification)
        => $"/media/{notification.MediaId}";

    private static string GenerateGroupNotificationUrl(GroupNotification groupNotification)
        => $"/groups/{groupNotification.GroupId}";

    private static string GenerateStoryNotificationUrl(StoryNotification storyNotification)
        => $"/stories/{storyNotification.StoryId}";

    private static string GenerateNetworkNotificationUrl(NetworkNotification networkNotification)
        => networkNotification.Type switch
        {
            NotificationType.FriendAccepted => $"/friends/{networkNotification.ActorId}",
            NotificationType.FriendRequest => $"/friend-requests",
            NotificationType.ProfileView => $"/profile/{networkNotification.ActorId}",
            NotificationType.BirthdayReminder => $"/profile/{networkNotification.ActorId}",
            NotificationType.StartFollowing => $"/profile/{networkNotification.ActorId}",
            _ => "/network"
        };

    private static string GeneratePostNotificationUrl(PostNotification postNotification)
        => $"/posts/{postNotification.PostId}";
}