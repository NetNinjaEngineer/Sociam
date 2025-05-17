namespace Sociam.Domain.Utils;

public abstract class SortableParams : PagedParams
{
    public string? Sort { get; set; }
}