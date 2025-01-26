using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Hubs.Interfaces;

namespace Sociam.Application.Hubs;

[Authorize]
public sealed class NotificationHub : Hub<INotificationClient>
{
    public async Task SendNotification(string friendId, string message)
    {
        await Clients.User(friendId).ReceiveNotification(message);
    }
}
