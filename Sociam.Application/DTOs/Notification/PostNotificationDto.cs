namespace Sociam.Application.DTOs.Notification;

public sealed class PostNotificationDto
{
    public Guid? PostId { get; set; }
    public string? PostContent { get; set; }
}