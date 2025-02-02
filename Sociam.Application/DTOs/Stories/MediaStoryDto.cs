namespace Sociam.Application.DTOs.Stories;

public sealed class MediaStoryDto : BaseStoryDto
{
    public string? MediaUrl { get; set; }
    public string? MediaType { get; set; }
    public string? Caption { get; set; }
}