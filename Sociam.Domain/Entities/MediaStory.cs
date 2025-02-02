using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class MediaStory : Story
{
    public string? Caption { get; set; }
    public string MediaUrl { get; set; } = null!;
    public MediaType MediaType { get; set; }
}