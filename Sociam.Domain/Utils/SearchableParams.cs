namespace Sociam.Domain.Utils;

public abstract class SearchableParams : SortableParams
{
    public string? SearchTerm { get; set; }
}