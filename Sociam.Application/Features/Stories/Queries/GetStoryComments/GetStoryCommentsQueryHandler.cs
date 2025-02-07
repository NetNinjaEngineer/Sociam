using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryComments;
public sealed class GetStoryCommentsQueryHandler(IStoryService service)
    : IRequestHandler<GetStoryCommentsQuery, Result<StoryWithCommentsResponseDto?>>
{
    public async Task<Result<StoryWithCommentsResponseDto?>> Handle(
        GetStoryCommentsQuery request, CancellationToken cancellationToken)
        => await service.GetStoryWithCommentsAsync(request);
}
