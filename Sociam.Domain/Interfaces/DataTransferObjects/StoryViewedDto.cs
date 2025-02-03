using Sociam.Domain.Enums;

namespace Sociam.Domain.Interfaces.DataTransferObjects;

public class StoryViewedDto
{
    public Guid StoryId { get; set; }
    public string CreatorId { get; set; } = string.Empty;
    public string CreatorFullName { get; set; } = string.Empty;
    public string? CreatorProfilePicture { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public int ReactionsCount { get; set; }
    public int CommentsCount { get; set; }
    public int ViewsCount { get; set; }
    public string? Content { get; set; }
    public string? MediaUrl { get; set; }
    public MediaType? MediaType { get; set; }
    public string? Caption { get; set; }
    public List<string>? HashTags { get; set; }
}