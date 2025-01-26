using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Features.FriendRequests.Commands.CurrentUserSendFriendRequest;
using Sociam.Application.Hubs.Interfaces;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Hubs;
[Authorize]
public sealed class FriendRequestHub(
    IFriendshipService friendshipService) : Hub<IFriendRequestClient>
{
    public async Task SendFriendRequest(string friendId)
    {
        var result = await friendshipService.SendFriendRequestCurrentUserAsync(
            CurrentUserSendFriendRequestCommand.Get(friendId));

        if (result.IsSuccess)
            await Clients.User(friendId).ReceiveFriendRequest($"{result.Value.Requester} sent you a friend request at {result.Value.CreatedAt}");
    }
}
