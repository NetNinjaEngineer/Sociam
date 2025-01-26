using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Queries.CheckIfAreFriends;
public sealed class CheckIfAreFriendsQueryHandler(IFriendshipService friendshipService) : IRequestHandler<CheckIfAreFriendsQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(CheckIfAreFriendsQuery request, CancellationToken cancellationToken)
        => await friendshipService.AreFriendsAsync(request);
}
