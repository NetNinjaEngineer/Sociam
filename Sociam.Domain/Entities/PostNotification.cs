namespace Sociam.Domain.Entities;

public sealed class PostNotification : Notification
{
    public Guid PostId { get; set; }
    public string? PostContent { get; set; }

    //public override string GenerateNotificationText(string senderName)
    //    => Type switch
    //    {
    //        NotificationType.NewPostCreated => $"{senderName} created a new post",
    //        NotificationType.PostReaction => $"{senderName} reached to your post",
    //        NotificationType.PostComment => $"{senderName} commented on your post: {PostContent?[50..]}",
    //        NotificationType.PostShare => $"{senderName} shared your post",
    //        NotificationType.MentionedInPost => $"{senderName} mentioned you in a post: {PostContent?[50..]}",
    //        NotificationType.TaggedInPost => $"{senderName} tagged you in a post: {PostContent?[50..]}",
    //        _ => "New post activity"
    //    };
}