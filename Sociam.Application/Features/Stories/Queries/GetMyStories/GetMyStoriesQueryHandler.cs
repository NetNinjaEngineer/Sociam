using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetMyStories;
public sealed class GetMyStoriesQueryHandler(
    IStoryService service) : IRequestHandler<GetMyStoriesQuery, Result<IEnumerable<StoryDto>>>
{
    public async Task<Result<IEnumerable<StoryDto>>> Handle(
        GetMyStoriesQuery request, CancellationToken cancellationToken)
        => await service.GetMyStoriesAsync();
}
