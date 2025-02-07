namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryReactionResponseDto
{
    public string ReactedById { get; set; } = null!;
    public string ReactedBy { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public DateTimeOffset ReactedAt { get; set; }
    public string ReactionType { get; set; } = string.Empty;
}