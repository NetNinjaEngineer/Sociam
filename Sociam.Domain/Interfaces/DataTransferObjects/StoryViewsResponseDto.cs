using Sociam.Domain.Enums;

namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryViewsResponseDto
{
    public Guid StoryId { get; set; }
    public string CreatorId { get; set; } = string.Empty;
    public string CreatorFirstName { get; set; } = string.Empty;
    public string CreatorLastName { get; set; } = string.Empty;
    public string CreatorFullName => $"{CreatorFirstName} {CreatorLastName}";
    public string CreatorUserName { get; set; } = string.Empty;
    public string? CreatorProfilePicture { get; set; }
    public string? Content { get; set; }
    public List<string>? HashTags { get; set; }
    public MediaType? MediaType { get; set; }
    public string? MediaUrl { get; set; }
    public string? Caption { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public int ReactionsCount { get; set; }
    public int CommentsCount { get; set; }
    public int ViewsCount { get; set; }

    public List<StoryViewerDto> Viewers { get; set; } = [];
}