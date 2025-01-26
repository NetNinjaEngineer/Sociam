using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;

namespace Sociam.Application.Features.FriendRequests.Queries.GetLoggedInUserRequestedFriendships;
public sealed class GetLoggedInUserRequestedFriendshipsQuery : IRequest<Result<IEnumerable<PendingFriendshipRequest>>>
{
}