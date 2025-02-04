using Sociam.Domain.Enums;

namespace Sociam.Domain.Utils;

public sealed class StoryQueryParameters
{
    private const int MaxPageSize = 50;
    private const int DefaultPageSize = 10;
    private int _pageSize = DefaultPageSize;
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Contains { get; set; }
    public MediaType? MediaType { get; set; }
    public List<string>? Hashtags { get; set; }
    public StoryPrivacy? Privacy { get; set; }
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value <= 0 ? DefaultPageSize : value >= MaxPageSize ? MaxPageSize : value;
    }
}