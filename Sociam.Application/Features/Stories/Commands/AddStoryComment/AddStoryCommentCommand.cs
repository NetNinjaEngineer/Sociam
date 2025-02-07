using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Stories.Commands.AddStoryComment;
public sealed class AddStoryCommentCommand : IRequest<Result<bool>>
{
    public Guid StoryId { get; set; }
    public string Comment { get; set; } = null!;
}
