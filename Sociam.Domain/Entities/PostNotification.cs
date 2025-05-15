namespace Sociam.Domain.Entities;

public sealed class PostNotification : Notification
{
    public Guid PostId { get; set; }
    public string? PostContent { get; set; }
}