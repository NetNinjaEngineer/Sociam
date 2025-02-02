namespace Sociam.Domain.Entities;

public sealed class TextStory : Story
{
    public string? Content { get; set; }
    public List<string>? HashTags { get; set; } = [];

}