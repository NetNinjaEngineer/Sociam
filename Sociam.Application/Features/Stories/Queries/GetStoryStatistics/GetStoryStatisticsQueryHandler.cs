using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryStatistics;

public sealed class GetStoryStatisticsQueryHandler(IStoryService service)
    : IRequestHandler<GetStoryStatisticsQuery, Result<StoryStatisticsDto?>>
{
    public async Task<Result<StoryStatisticsDto?>> Handle(
        GetStoryStatisticsQuery request, CancellationToken cancellationToken)
        => await service.GetStoryStatisticsAsync(request);
}