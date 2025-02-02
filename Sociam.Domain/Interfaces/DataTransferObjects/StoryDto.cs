using Sociam.Domain.Enums;

namespace Sociam.Domain.Interfaces.DataTransferObjects;
public sealed class StoryDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public StoryPrivacy StoryPrivacy { get; set; }
    public string UserId { get; set; } = null!;
    public string? Content { get; set; }
    public List<string>? HashTags { get; set; }
    public string? Caption { get; set; }
    public string? MediaUrl { get; set; }
    public MediaType? MediaType { get; set; }
    public string StoryType { get; set; } = null!;
}