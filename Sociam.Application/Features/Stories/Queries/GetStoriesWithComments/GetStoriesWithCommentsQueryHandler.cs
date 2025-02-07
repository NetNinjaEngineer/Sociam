using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoriesWithComments;
public sealed class GetStoriesWithCommentsQueryHandler(IStoryService service)
    : IRequestHandler<GetStoriesWithCommentsQuery, Result<IEnumerable<StoryWithCommentsResponseDto>>>
{
    public async Task<Result<IEnumerable<StoryWithCommentsResponseDto>>> Handle(GetStoriesWithCommentsQuery request, CancellationToken cancellationToken)
    {
        return await service.GetStoriesWithCommentsAsync();
    }
}
