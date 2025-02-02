namespace Sociam.Domain.Entities;

public sealed class TextStory : Story
{
    public List<string>? HashTags { get; set; } = [];

}