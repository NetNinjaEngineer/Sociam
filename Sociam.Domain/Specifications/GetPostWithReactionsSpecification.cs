using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetPostWithReactionsSpecification : BaseSpecification<Post>
{
    public GetPostWithReactionsSpecification(Guid postId) : base(post => post.Id == postId)
    {
        AddIncludes(m => m.Reactions);
    }
}