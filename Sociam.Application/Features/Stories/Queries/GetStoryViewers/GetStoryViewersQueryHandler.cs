using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryViewers;
public sealed class GetStoryViewersQueryHandler(IStoryService service)
    : IRequestHandler<GetStoryViewersQuery, Result<StoryViewsResponseDto?>>
{
    public async Task<Result<StoryViewsResponseDto?>> Handle(
        GetStoryViewersQuery request, CancellationToken cancellationToken)
        => await service.GetStoryViewsAsync(request);
}
