using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;
public sealed class Friendship : BaseEntity
{
    public string RequesterId { get; set; } = null!;

    public ApplicationUser Requester { get; set; } = null!;

    public string ReceiverId { get; set; }

    public ApplicationUser Receiver { get; set; } = null!;

    public FriendshipStatus FriendshipStatus { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset? UpdatedAt { get; set; }
}
