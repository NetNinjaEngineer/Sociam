using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryReactions;
public sealed class GetStoryReactionsQueryHandler(IStoryService service)
    : IRequestHandler<GetStoryReactionsQuery, Result<StoryWithReactionsResponseDto?>>
{
    public async Task<Result<StoryWithReactionsResponseDto?>> Handle(
        GetStoryReactionsQuery request, CancellationToken cancellationToken)
        => await service.GetStoryWithReactionsAsync(request);
}
