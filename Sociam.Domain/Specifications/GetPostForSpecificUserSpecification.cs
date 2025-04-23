using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetPostForSpecificUserSpecification : BaseSpecification<Post>
{
    public GetPostForSpecificUserSpecification(Guid postId, string userId): base(post => post.Id == postId && post.CreatedById == userId)
    {
        AddIncludes(p => p.Media);
    }
}