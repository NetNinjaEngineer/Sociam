using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Queries.GetFriends;
public sealed class GetFriendsQueryHandler(IFriendshipService service)
    : IRequestHandler<GetFriendsQuery, Result<List<UserProfileDto>>>
{
    public async Task<Result<List<UserProfileDto>>> Handle(
        GetFriendsQuery request, CancellationToken cancellationToken) => await service.GetFriendsAsync();
}
