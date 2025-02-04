using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryArchive;
public sealed class GetStoryArchiveQueryHandler(IStoryService service)
    : IRequestHandler<GetStoryArchiveQuery, Result<PagedResult<StoryViewsResponseDto>>>
{
    public async Task<Result<PagedResult<StoryViewsResponseDto>>> Handle(
        GetStoryArchiveQuery request, CancellationToken cancellationToken)
        => await service.GetStoryArchiveAsync(request.QueryParameters);
}
