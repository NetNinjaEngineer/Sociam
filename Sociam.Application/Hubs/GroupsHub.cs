using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Hubs.Interfaces;

namespace Sociam.Application.Hubs;

[Authorize]
public sealed class GroupsHub : Hub<IGroupsClient>
{
    public async Task SendAddUserToGroupAsync(string userId, string message)
        => await Clients.User(userId).ReceiveAddedToGroup(message);

}
