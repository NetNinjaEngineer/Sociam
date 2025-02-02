namespace Sociam.Application.DTOs.Stories;

public sealed class TextStoryDto :BaseStoryDto
{
    public string Content { get; set; } = string.Empty;
    public List<string>? HashTags { get; set; }

}