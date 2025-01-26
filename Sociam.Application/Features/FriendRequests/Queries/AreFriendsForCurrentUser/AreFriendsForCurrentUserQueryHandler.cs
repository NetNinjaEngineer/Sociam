using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Queries.AreFriendsForCurrentUser;
public sealed class AreFriendsForCurrentUserQueryHandler(IFriendshipService friendshipService)
    : IRequestHandler<AreFriendsForCurrentUserQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(AreFriendsForCurrentUserQuery request, CancellationToken cancellationToken)
        => await friendshipService.AreFriendsWithCurrentUserAsync(request);
}
