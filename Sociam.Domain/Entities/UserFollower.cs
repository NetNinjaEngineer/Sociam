using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;
public sealed class UserFollower : BaseEntity
{
    public string FollowedUserId { get; set; } = null!;

    public string FollowerUserId { get; set; } = null!;

    public DateTimeOffset FollowedAt { get; set; }

    public ApplicationUser FollowedUser { get; set; } = null!;

    public ApplicationUser FollowerUser { get; set; } = null!;
}
