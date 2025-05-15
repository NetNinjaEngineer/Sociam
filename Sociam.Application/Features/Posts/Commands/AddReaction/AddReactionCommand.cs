using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Posts.Commands.AddReaction;

public sealed class AddReactionCommand : IRequest<Result<bool>>
{
    public Guid PostId { get; set; }
    public ReactionType Reaction { get; set; }
}
