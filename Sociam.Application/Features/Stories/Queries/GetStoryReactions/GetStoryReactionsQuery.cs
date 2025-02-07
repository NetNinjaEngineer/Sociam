using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryReactions;
public sealed class GetStoryReactionsQuery : IRequest<Result<StoryWithReactionsResponseDto?>>
{
    public Guid StoryId { get; set; }
}
