using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Posts.Commands.AddReaction;

public sealed class AddReactionCommandHandler(IPostsService service)
    : IRequestHandler<AddReactionCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AddReactionCommand request, CancellationToken cancellationToken)
      => await service.AddReactionAsync(request);
}
