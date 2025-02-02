using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Queries.HasUnseenStories;
public sealed class HasUnseenStoriesQueryHandler(IStoryService service)
    : IRequestHandler<HasUnseenStoriesQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(HasUnseenStoriesQuery request, CancellationToken cancellationToken)
    {
        return await service.HasUnseenStoriesAsync(request);
    }
}
