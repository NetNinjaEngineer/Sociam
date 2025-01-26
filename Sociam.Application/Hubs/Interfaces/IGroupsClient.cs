namespace Sociam.Application.Hubs.Interfaces;
public interface IGroupsClient
{
    Task ReceiveAddedToGroup(string message);
}
