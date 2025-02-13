namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class ViewerBreakdownDto
{
    public string ViewerId { get; set; } = string.Empty;
    public string ViewerName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public DateTimeOffset? ViewedAt { get; set; }
    public string ViewerType { get; set; } = string.Empty;
}