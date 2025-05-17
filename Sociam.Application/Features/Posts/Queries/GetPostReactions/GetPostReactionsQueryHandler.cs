using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Posts.Queries.GetPostReactions;

public sealed class GetPostReactionsQueryHandler(IPostsService service)
    : IRequestHandler<GetPostReactionsQuery, Result<PagedResult<PostReactionDto>>>
{
    public async Task<Result<PagedResult<PostReactionDto>>> Handle(
        GetPostReactionsQuery request, CancellationToken cancellationToken)
        => await service.GetPostReactionsAsync(request);
}
