using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Posts.Queries.GetPost;

public sealed class GetPostQueryHandler(IPostsService service) : IRequestHandler<GetPostQuery, Result<PostDto>>
{
    public async Task<Result<PostDto>> Handle(GetPostQuery request, CancellationToken cancellationToken)
        => await service.GetMyPostAsync(request);
}
