using Sociam.Domain.Entities;

namespace Sociam.Domain.Specifications;

public sealed class GetPostWithMediaSpecification : BaseSpecification<Post>
{
    public GetPostWithMediaSpecification(Guid postId) : base(post => post.Id == postId)
    {
        AddIncludes(m => m.Media);
    }
}