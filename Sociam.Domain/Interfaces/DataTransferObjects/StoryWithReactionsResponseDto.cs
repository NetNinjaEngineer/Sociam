namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryWithReactionsResponseDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiredAt { get; set; }
    public bool IsExpired => DateTimeOffset.Now >= ExpiredAt;
    public bool IsArchived { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Privacy { get; set; } = string.Empty;
    public string? Content { get; set; }
    public List<string>? HashTags { get; set; }
    public string? Caption { get; set; }
    public string? MediaUrl { get; set; }
    public string? MediaType { get; set; }
    public IEnumerable<StoryReactionResponseDto> Reactions { get; set; } = [];
}