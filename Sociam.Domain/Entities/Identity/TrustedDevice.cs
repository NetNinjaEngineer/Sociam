namespace Sociam.Domain.Entities.Identity;
public sealed class TrustedDevice
{
    public Guid Id { get; set; }
    public string DeviceName { get; set; } = null!;
    public string DeviceId { get; set; } = null!;
    public bool IsActive { get; set; }
    public string Location { get; set; } = null!;
    public string IpAddress { get; set; } = null!;
    public string UserAgent { get; set; } = null!;
    public DateTimeOffset LastLogin { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ExpiryDate { get; set; }
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
