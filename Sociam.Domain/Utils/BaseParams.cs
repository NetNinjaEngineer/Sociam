namespace Sociam.Domain.Utils;

public abstract class BaseParams
{
    private string? _searchTerm;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? SearchTerm
    {
        get => _searchTerm;
        set => _searchTerm = value?.Trim().ToLowerInvariant();
    }

    public string? Sort { get; set; }
    public bool EnablePaging { get; set; }
}
