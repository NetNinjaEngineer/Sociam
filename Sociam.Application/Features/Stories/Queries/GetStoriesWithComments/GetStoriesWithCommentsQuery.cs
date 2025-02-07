using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoriesWithComments;
public sealed class GetStoriesWithCommentsQuery : IRequest<Result<IEnumerable<StoryWithCommentsResponseDto>>>
{
}
