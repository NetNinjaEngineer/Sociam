using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;

namespace Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
public sealed class GetActiveFriendStoriesQuery : IRequest<Result<IEnumerable<StoryDto>>>
{
}
