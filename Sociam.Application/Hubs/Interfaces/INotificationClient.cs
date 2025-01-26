namespace Sociam.Application.Hubs.Interfaces;
public interface INotificationClient
{
    Task ReceiveNotification(string message);
}
