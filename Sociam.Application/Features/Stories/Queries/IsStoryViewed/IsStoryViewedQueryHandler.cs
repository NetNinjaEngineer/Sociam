using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Queries.IsStoryViewed;
public sealed class IsStoryViewedQueryHandler(IStoryService service)
    : IRequestHandler<IsStoryViewedQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        IsStoryViewedQuery request, CancellationToken cancellationToken)
        => await service.IsStoryViewedAsync(request);
}
