using Sociam.Domain.Enums;

namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryForReturnDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public StoryPrivacy StoryPrivacy { get; set; }
    public int ViewersCount { get; set; }
    public int ReactionsCount { get; set; }
    public int CommentsCount { get; set; }
    public string? Content { get; set; }
    public List<string>? HashTags { get; set; }
    public string? Caption { get; set; }
    public string? MediaUrl { get; set; }
    public MediaType? MediaType { get; set; }
}