using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetMyStories;
public sealed class GetMyStoriesQuery : IRequest<Result<IEnumerable<StoryDto>>>
{
}
