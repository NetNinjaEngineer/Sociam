using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryStatistics;

public sealed class GetStoryStatisticsQuery : IRequest<Result<StoryStatisticsDto?>>
{
    public Guid StoryId { get; set; }
}