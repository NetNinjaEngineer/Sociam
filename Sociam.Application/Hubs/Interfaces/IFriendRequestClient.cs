namespace Sociam.Application.Hubs.Interfaces;
public interface IFriendRequestClient
{
    Task ReceiveFriendRequest(string message);
}
