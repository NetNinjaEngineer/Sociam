using Sociam.Domain.Entities;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Specifications;

public sealed class GetPostsForSpecificUserSpecification : BaseSpecification<Post>
{
    public GetPostsForSpecificUserSpecification(string userId, PostsParams @postsParams): 
        base(post => post.CreatedById == userId && (
            string.IsNullOrEmpty(@postsParams.SearchTerm) ||
            string.IsNullOrEmpty(post.Text) ||
            post.Text.ToLower().Contains(@postsParams.SearchTerm) ))
    {
        AddIncludes(post => post.OriginalPost!);
        AddIncludes(post => post.CreatedBy);

        if (!string.IsNullOrEmpty(@postsParams.Sort)){}
        ApplyPaging(@postsParams.Page, @postsParams.PageSize);
    }
}