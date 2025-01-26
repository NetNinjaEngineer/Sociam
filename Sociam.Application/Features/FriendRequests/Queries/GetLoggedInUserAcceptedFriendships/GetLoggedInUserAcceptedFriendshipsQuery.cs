using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;

namespace Sociam.Application.Features.FriendRequests.Queries.GetLoggedInUserAcceptedFriendships;
public sealed class GetLoggedInUserAcceptedFriendshipsQuery : IRequest<Result<IEnumerable<GetUserAcceptedFriendshipDto>>>
{
}
