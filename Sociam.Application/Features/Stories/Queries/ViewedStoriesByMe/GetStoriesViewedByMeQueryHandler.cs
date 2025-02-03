using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.ViewedStoriesByMe;
public sealed class GetStoriesViewedByMeQueryHandler(IStoryService service)
    : IRequestHandler<GetStoriesViewedByMeQuery, Result<List<StoryViewedDto>>>
{
    public async Task<Result<List<StoryViewedDto>>> Handle(
        GetStoriesViewedByMeQuery request, CancellationToken cancellationToken)
        => await service.GetStoriesViewedByMeAsync();
}
