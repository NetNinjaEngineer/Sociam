using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class NetworkNotification : Notification
{
    public override string GenerateNotificationText(string senderName)
        => Type switch
        {
            NotificationType.FriendRequest => $"{senderName} sent you a friend request",
            NotificationType.FriendAccepted => $"{senderName} accepted your friend request",
            NotificationType.ProfileView => $"{senderName} viewed your profile",
            NotificationType.BirthdayReminder => $"Today is {senderName}'s birthday",
            NotificationType.StartFollowing => $"{senderName} start following you",
            _ => "New network activity"
        };
}