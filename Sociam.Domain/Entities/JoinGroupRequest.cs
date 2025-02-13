using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class JoinGroupRequest : BaseEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;
    public DateTimeOffset RequestedAt { get; set; } = DateTimeOffset.UtcNow;
    public string RequestorId { get; set; } = null!;
    public ApplicationUser Requestor { get; set; } = null!;
    public JoinRequestStatus Status { get; set; }
}