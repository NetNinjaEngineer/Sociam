using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.AddStoryReaction;
public sealed class AddStoryReactionCommand : IRequest<Result<bool>>
{
    public Guid StoryId { get; set; }
    public ReactionType ReactionType { get; set; }
}
