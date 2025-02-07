namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryStatisticsDto
{
    public Guid StoryId { get; set; }
    public string? MediaUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public bool IsExpired => DateTimeOffset.Now >= ExpiresAt;
    public bool IsArchived { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Privacy { get; set; } = string.Empty;
    public string? Content { get; set; }
    public List<string>? HashTags { get; set; }
    public string? Caption { get; set; }
    public string? MediaType { get; set; }
    public int TotalViews { get; set; }
    public int UniqueViews { get; set; }
    public int TotalComments { get; set; }
    public int TotalReactions { get; set; }
    public Dictionary<string, int> ReactionsBreakdown { get; set; } = [];
    public List<ViewerBreakdownDto> Viewers { get; set; } = [];
    public List<StoryCommenterResponseDto> Comments { get; set; } = [];
    public List<StoryReactionResponseDto> Reactions { get; set; } = [];
    public Dictionary<string, int> ViewersByAgeGroup { get; set; } = [];
    public Dictionary<string, int> GetViewersByGender { get; set; } = [];
}