using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;

namespace Sociam.Application.Features.FriendRequests.Queries.GetFriends;
public sealed class GetFriendsQuery : IRequest<Result<List<UserProfileDto>>>
{
}
