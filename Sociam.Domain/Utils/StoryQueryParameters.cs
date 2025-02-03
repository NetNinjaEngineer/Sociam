using Sociam.Domain.Enums;

namespace Sociam.Domain.Utils;

public sealed class StoryQueryParameters
{
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Contains { get; set; }
    public MediaType? MediaType { get; set; }
    public List<string>? Hashtags { get; set; }
    public StoryPrivacy? Privacy { get; set; }
}