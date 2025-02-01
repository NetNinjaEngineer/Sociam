using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Stories.Commands.DeleteStory;
public sealed class DeleteStoryCommand : IRequest<Result<bool>>
{
    public Guid StoryId { get; set; }
}
