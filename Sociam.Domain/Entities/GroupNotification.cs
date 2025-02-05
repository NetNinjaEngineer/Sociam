using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public class GroupNotification : Notification
{
    public required string GroupId { get; set; }
    public required string GroupName { get; set; }
    public string? GroupRole { get; set; }

    public override string GenerateNotificationText(string senderName)
    {
        return Type switch
        {
            NotificationType.GroupInvite => $"{senderName} invited you to join {GroupName}",
            NotificationType.GroupJoinRequest => $"{senderName} requested to join {GroupName}",
            NotificationType.GroupPostActivity => $"New activity in {GroupName}",
            NotificationType.GroupRoleChange => $"Your role in {GroupName} has been updated to {GroupRole}",
            _ => "New group activity"
        };
    }
}