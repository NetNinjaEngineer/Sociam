using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;

namespace Sociam.Application.Features.Stories.Queries.GetMyStories;
public sealed class GetMyStoriesQuery : IRequest<Result<IEnumerable<StoryDto>>>
{
}
