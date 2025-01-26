using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Followings.Commands.UnFollowUser;
public sealed class UnFollowUserCommandHandler(IFollowingService followingService) : IRequestHandler<UnFollowUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UnFollowUserCommand request,
        CancellationToken cancellationToken)
        => await followingService.UnfollowUserAsync(request.FollowerId, request.FollowedId);
}
