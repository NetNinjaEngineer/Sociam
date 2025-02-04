using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetExpiredStories;
public sealed class GetExpiredStoriesQueryHandler(IStoryService service)
    : IRequestHandler<GetExpiredStoriesQuery, Result<PagedResult<StoryViewsResponseDto>>>
{
    public async Task<Result<PagedResult<StoryViewsResponseDto>>> Handle(
        GetExpiredStoriesQuery request, CancellationToken cancellationToken)
        => await service.GetExpiredStoriesAsync(request.QueryParameters);
}
