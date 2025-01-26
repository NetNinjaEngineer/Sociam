using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.FriendRequests.Queries.AreFriendsForCurrentUser;
public sealed class AreFriendsForCurrentUserQuery : IRequest<Result<bool>>
{
    public string FriendId { get; set; } = null!;
}
