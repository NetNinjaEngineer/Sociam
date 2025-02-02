namespace Sociam.Application.DTOs.Stories;
public sealed class StoryDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string? MediaUrl { get; set; }
    public string? MediaType { get; set; }
    public string? Caption { get; set; }
    public List<string>? HashTags { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
}
