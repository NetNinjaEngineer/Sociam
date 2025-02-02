using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Interfaces;
public interface IFriendshipRepository : IGenericRepository<Friendship>
{
    Task<Friendship?> GetFriendshipAsync(string user1Id, string user2Id);
    Task<List<Friendship>> GetUserFriendshipsAsync(string userId);
    Task<List<Friendship>> GetSentFriendRequestsAsync(string requesterId);
    Task<List<Friendship>> GetReceivedFriendRequestsAsync(string receiverId);
    Task<bool> AreFriendsAsync(string user1Id, string user2Id);
    Task<int> GetFriendsCountAsync(string userId);
    Task<List<Friendship>> GetAcceptedFriendshipsForUserAsync(string userId);
    Task<List<ApplicationUser>> GetFriendsOfUserAsync(string userId);
    Task<List<string>> GetFriendIdsForUserAsync(string userId);

}