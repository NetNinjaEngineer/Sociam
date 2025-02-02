using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
public sealed class GetActiveFriendStoriesQuery : IRequest<Result<IEnumerable<StoryDto>>>
{
}
