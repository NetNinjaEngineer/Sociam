using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;
public interface IPrivateConversationRepository : IGenericRepository<Conversation>
{
    Task<PrivateConversation?> GetPrivateUserConversationsAsync(string userId);
    Task<PrivateConversation?> GetPrivateConversationBetweenAsync(string senderId, string receiverId);
}
