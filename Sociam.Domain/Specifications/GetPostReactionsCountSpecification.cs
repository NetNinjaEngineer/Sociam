using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetPostReactionsCountSpecification(Guid postId) : BaseSpecification<PostReaction>(pr => pr.PostId == postId);