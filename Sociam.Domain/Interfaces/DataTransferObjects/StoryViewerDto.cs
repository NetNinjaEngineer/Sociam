namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryViewerDto
{
    public string ViewerId { get; set; } = string.Empty;
    public string ViewerName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; } = string.Empty;
    public DateTimeOffset? ViewedAt { get; set; }
}