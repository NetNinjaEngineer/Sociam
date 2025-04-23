using Sociam.Domain.Entities;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Specifications;

public sealed class GetPostsForUserWithFilterationCountSpecification(string userId, PostsParams @postsParams)
    : BaseSpecification<Post>(post => post.CreatedById == userId && (
        string.IsNullOrEmpty(@postsParams.SearchTerm) ||
        string.IsNullOrEmpty(post.Text) ||
        post.Text.ToLower().Contains(@postsParams.SearchTerm)));