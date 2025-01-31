using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Queries.GetCurrentUserReceivedFriendRequests;
public sealed class GetCurrentUserReceivedFriendRequestsQueryHandler(IFriendshipService service)
    : IRequestHandler<GetCurrentUserReceivedFriendRequestsQuery, Result<IEnumerable<PendingFriendshipRequest>>>
{
    public async Task<Result<IEnumerable<PendingFriendshipRequest>>> Handle(
        GetCurrentUserReceivedFriendRequestsQuery request, CancellationToken cancellationToken)
        => await service.GetCurrentUserReceivedFriendRequestsAsync();
}
