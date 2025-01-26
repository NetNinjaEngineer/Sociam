using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Queries.GetLoggedInUserAcceptedFriendships;
public sealed class GetLoggedInUserAcceptedFriendshipsQueryHandler(IFriendshipService friendshipService)
    : IRequestHandler<GetLoggedInUserAcceptedFriendshipsQuery, Result<IEnumerable<GetUserAcceptedFriendshipDto>>>
{
    public async Task<Result<IEnumerable<GetUserAcceptedFriendshipDto>>> Handle(
        GetLoggedInUserAcceptedFriendshipsQuery request,
        CancellationToken cancellationToken)
        => await friendshipService.GetLoggedInUserAcceptedFriendshipsAsync();
}
