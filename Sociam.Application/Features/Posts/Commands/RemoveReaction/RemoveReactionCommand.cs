using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Posts.Commands.RemoveReaction;

public sealed class RemoveReactionCommand : IRequest<Result<bool>>
{
    public Guid PostId { get; set; }
    public Guid ReactionId { get; set; }
}
