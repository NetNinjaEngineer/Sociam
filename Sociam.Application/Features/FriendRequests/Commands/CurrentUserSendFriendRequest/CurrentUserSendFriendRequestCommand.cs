using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;

namespace Sociam.Application.Features.FriendRequests.Commands.CurrentUserSendFriendRequest;
public sealed class CurrentUserSendFriendRequestCommand : IRequest<Result<FriendshipResponseDto>>
{
    public string FriendId { get; set; } = null!;

    public static CurrentUserSendFriendRequestCommand Get(string friendId) => new() { FriendId = friendId };
}
