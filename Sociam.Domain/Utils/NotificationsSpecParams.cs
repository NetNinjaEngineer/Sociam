namespace Sociam.Domain.Utils;

public sealed class NotificationsSpecParams : SearchableParams
{
    public bool? IsRead { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}