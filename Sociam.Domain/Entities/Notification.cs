using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public class Notification : BaseEntity
{
    public required string RecipientId { get; set; }
    public required string ActorId { get; set; }
    public NotificationType Type { get; set; }
    public NotificationStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ReadAt { get; set; }
    public string Message { get; set; } = null!;
    public string? ActionUrl { get; set; }
    public ApplicationUser Recipient { get; set; } = null!;
    public ApplicationUser Actor { get; set; } = null!;
}