using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Posts.Commands.RemoveReaction;

public sealed class RemoveReactionCommandHandler(IPostsService service) : IRequestHandler<RemoveReactionCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RemoveReactionCommand request, CancellationToken cancellationToken)
        => await service.RemoveReactionAsync(request);
}
