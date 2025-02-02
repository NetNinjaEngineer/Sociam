using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Application.Features.Stories.Queries.GetStoryById;
public sealed class GetStoryByIdQuery : IRequest<Result<StoryDto>>
{
    public Guid StoryId { get; set; }
}
