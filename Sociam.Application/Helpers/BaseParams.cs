namespace Sociam.Application.Helpers;

public abstract class BaseParams
{
    public int Page { get; set; } = 1;
    private int _pageSize = 5;
    public int PageSize { get => _pageSize; set => _pageSize = value > 10 ? 10 : value; }
    private string? _searchTerm;
    public string? SearchTerm { get => _searchTerm; set => _searchTerm = value?.Trim().ToLower(); }
    public string? Sort { get; set; }
}