using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryById;
public sealed class GetStoryByIdQueryHandler(
    IStoryService service) : IRequestHandler<GetStoryByIdQuery, Result<StoryDto>>
{
    public async Task<Result<StoryDto>> Handle(GetStoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await service.GetStoryAsync(request);
    }
}
