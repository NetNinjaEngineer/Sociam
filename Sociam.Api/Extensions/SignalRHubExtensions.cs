using Sociam.Application.Hubs;

namespace Sociam.Api.Extensions;

public static class SignalRHubExtensions
{
    public static IApplicationBuilder UseHubs(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/hubs/notifications");

        app.MapHub<MessageHub>("/hubs/messages");

        app.MapHub<FriendRequestHub>("/hubs/friendRequests");

        app.MapHub<GroupsHub>("/hubs/groups");

        return app;
    }
}
