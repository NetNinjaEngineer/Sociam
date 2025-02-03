using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.ViewedStoriesByMe;
public sealed class GetStoriesViewedByMeQuery : IRequest<Result<List<StoryViewedDto>>>
{
}
