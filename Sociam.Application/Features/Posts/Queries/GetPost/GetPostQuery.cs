using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;

namespace Sociam.Application.Features.Posts.Queries.GetPost;

public sealed class GetPostQuery(Guid postId) : IRequest<Result<PostDto>>
{
    public Guid PostId { get; } = postId;
}
