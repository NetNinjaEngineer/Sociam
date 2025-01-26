using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;
public interface IUnitOfWork : IAsyncDisposable
{
    IFriendshipRepository FriendshipRepository { get; }
    IMessageRepository MessageRepository { get; }
    IPrivateConversationRepository ConversationRepository { get; }
    IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> SaveChangesAsync();
}
