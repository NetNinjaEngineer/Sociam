using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Application.Features.Posts.Queries.GetPosts;

public sealed class GetPostsQuery : IRequest<Result<PagedResult<PostDto>>>
{
    public required PostsParams PostsParams { get; set; }
}