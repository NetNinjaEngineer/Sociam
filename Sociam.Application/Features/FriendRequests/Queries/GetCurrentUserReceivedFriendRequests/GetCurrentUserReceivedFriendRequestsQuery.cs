using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;

namespace Sociam.Application.Features.FriendRequests.Queries.GetCurrentUserReceivedFriendRequests;
public sealed class GetCurrentUserReceivedFriendRequestsQuery : IRequest<Result<IEnumerable<PendingFriendshipRequest>>>
{
}
