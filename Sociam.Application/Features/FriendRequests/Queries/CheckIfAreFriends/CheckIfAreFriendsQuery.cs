using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.FriendRequests.Queries.CheckIfAreFriends;
public sealed class CheckIfAreFriendsQuery : IRequest<Result<bool>>
{
    public string User1Id { get; set; } = null!;
    public string User2Id { get; set; } = null!;
}
