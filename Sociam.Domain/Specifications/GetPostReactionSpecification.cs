using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetPostReactionSpecification(Guid postId, string userId, Guid reactionId)
    : BaseSpecification<PostReaction>(pr => pr.Id == reactionId && pr.ReactedById == userId && pr.PostId == postId);