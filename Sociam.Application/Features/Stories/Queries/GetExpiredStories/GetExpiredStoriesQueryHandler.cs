using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetExpiredStories;
public sealed class GetExpiredStoriesQueryHandler(IStoryService service)
    : IRequestHandler<GetExpiredStoriesQuery, Result<IEnumerable<StoryViewsResponseDto>>>
{
    public async Task<Result<IEnumerable<StoryViewsResponseDto>>> Handle(
        GetExpiredStoriesQuery request, CancellationToken cancellationToken)
        => await service.GetExpiredStoriesAsync();
}
