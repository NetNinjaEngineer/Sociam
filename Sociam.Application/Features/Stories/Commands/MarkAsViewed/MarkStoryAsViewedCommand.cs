using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Stories.Commands.MarkAsViewed;
public sealed class MarkStoryAsViewedCommand : IRequest<Result<bool>>
{
    public Guid StoryId { get; set; }
}
