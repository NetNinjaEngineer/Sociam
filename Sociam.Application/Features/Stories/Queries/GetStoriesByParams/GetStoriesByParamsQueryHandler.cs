using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoriesByParams;

public sealed class GetStoriesByParamsQueryHandler(IStoryService service)
    : IRequestHandler<GetStoriesByParamsQuery, Result<PagedResult<StoryViewsResponseDto>>>
{
    public async Task<Result<PagedResult<StoryViewsResponseDto>>> Handle(
        GetStoriesByParamsQuery request, CancellationToken cancellationToken)
    {
        return await service.GetStoriesByParamsAsync(request);
    }
}
