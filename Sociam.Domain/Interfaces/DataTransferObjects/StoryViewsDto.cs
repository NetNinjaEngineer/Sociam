namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryViewsDto
{
    public string UserId { get; set; } = string.Empty;

    public Guid StoryId { get; set; }

    public string ViewerName { get; set; } = string.Empty;

    public string ProfilePictureUrl { get; set; } = string.Empty;

    public DateTimeOffset? ViewedAt { get; set; }
}