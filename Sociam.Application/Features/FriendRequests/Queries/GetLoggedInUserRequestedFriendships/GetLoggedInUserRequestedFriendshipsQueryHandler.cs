using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Queries.GetLoggedInUserRequestedFriendships;

public sealed class GetLoggedInUserRequestedFriendshipsQueryHandler(IFriendshipService friendshipService)
    : IRequestHandler<GetLoggedInUserRequestedFriendshipsQuery, Result<IEnumerable<PendingFriendshipRequest>>>
{
    public async Task<Result<IEnumerable<PendingFriendshipRequest>>> Handle(
        GetLoggedInUserRequestedFriendshipsQuery request,
        CancellationToken cancellationToken)
        => await friendshipService.GetCurrentUserSentFriendRequestsAsync();
}