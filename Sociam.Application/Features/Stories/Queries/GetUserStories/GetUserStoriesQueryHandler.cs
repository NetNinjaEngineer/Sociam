using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetUserStories;
public sealed class GetUserStoriesQueryHandler(IStoryService service)
    : IRequestHandler<GetUserStoriesQuery, Result<UserWithStoriesDto?>>
{
    public async Task<Result<UserWithStoriesDto?>> Handle(GetUserStoriesQuery request, CancellationToken cancellationToken)
    {
        return await service.GetActiveUserStoriesAsync(request);
    }
}
