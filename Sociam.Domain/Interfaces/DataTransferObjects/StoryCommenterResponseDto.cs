namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryCommenterResponseDto
{
    public string CommenterId { get; set; } = null!;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTimeOffset CommentedAt { get; set; }
}