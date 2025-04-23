using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Posts.Commands.DeletePost;

public sealed class DeletePostCommandHandler(IPostsService service) : IRequestHandler<DeletePostCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        => await service.DeletePostAsync(request);
}