using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryViewers;
public sealed class GetStoryViewersQuery : IRequest<Result<StoryViewsResponseDto?>>
{
    public Guid StoryId { get; set; }
}
