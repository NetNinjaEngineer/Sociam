using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Queries.GetMyStories;
public sealed class GetMyStoriesQueryHandler(
    IStoryService service) : IRequestHandler<GetMyStoriesQuery, Result<IEnumerable<StoryDto>>>
{
    public async Task<Result<IEnumerable<StoryDto>>> Handle(
        GetMyStoriesQuery request, CancellationToken cancellationToken)
        => await service.GetMyStoriesAsync();
}
