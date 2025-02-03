using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Application.Features.Stories.Queries.GetStoriesByParams;
public sealed class GetStoriesByParamsQuery : IRequest<Result<List<StoryViewsResponseDto>>>
{
    public StoryQueryParameters StoryQueryParameters { get; set; } = null!;
}
