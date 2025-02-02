using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
public sealed class GetActiveFriendStoriesQueryHandler(IStoryService storyService)
    : IRequestHandler<GetActiveFriendStoriesQuery, Result<IEnumerable<StoryDto>>>
{
    public async Task<Result<IEnumerable<StoryDto>>> Handle(
        GetActiveFriendStoriesQuery request, CancellationToken cancellationToken)
        => await storyService.GetActiveFriendStoriesAsync(request);
}
