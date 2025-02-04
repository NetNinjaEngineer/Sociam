using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetExpiredStories;
public sealed class GetExpiredStoriesQuery : IRequest<Result<IEnumerable<StoryViewsResponseDto>>>
{
}
