using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryComments;
public sealed class GetStoryCommentsQuery : IRequest<Result<StoryWithCommentsResponseDto?>>
{
    public Guid StoryId { get; set; }
}
