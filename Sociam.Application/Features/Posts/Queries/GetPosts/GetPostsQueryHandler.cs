using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Posts.Queries.GetPosts;

public sealed class GetPostsQueryHandler(IPostsService service) : IRequestHandler<GetPostsQuery, Result<PagedResult<PostDto>>>
{
    public async Task<Result<PagedResult<PostDto>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        => await service.GetPostsAsync(request);
}