using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Stories.Queries.IsStoryViewed;
public sealed class IsStoryViewedQuery : IRequest<Result<bool>>
{
    public Guid StoryId { get; set; }
}
