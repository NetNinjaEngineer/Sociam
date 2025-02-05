using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Utils;

//TODO: Still needs handling with frontend
public static class NotificationUrlGenerator
{
    public static string GenerateUrl(Notification notification)
        => notification switch
        {
            PostNotification postNotification => GeneratePostNotificationUrl(postNotification),
            NetworkNotification networkNotification => GenerateNetworkNotificationUrl(networkNotification),
            StoryNotification storyNotification => GenerateStoryNotificationUrl(storyNotification),
            GroupNotification groupNotification => GenerateGroupNotificationUrl(groupNotification),
            MediaNotification mediaNotification => GenerateMediaNotificationUrl(mediaNotification),
            _ => "/notifications"
        };


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