using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Followings.Commands.FollowUser;
public sealed class FollowUserCommandHandler(
    IFollowingService followingService) : IRequestHandler<FollowUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(FollowUserCommand request,
        CancellationToken cancellationToken)
        => await followingService.FollowUserAsync(request.FollowerId, request.FollowedId);
}
