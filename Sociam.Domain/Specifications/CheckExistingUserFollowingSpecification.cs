using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;
public sealed class CheckExistingUserFollowingSpecification(string followerId, string followedId)
    : BaseSpecification<UserFollower>(uf =>
        uf.FollowedUserId == followedId && uf.FollowerUserId == followerId);
