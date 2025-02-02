using Sociam.Domain.Entities.common;

namespace Sociam.Domain.Interfaces;
public interface IUnitOfWork : IAsyncDisposable
{
    IGroupMemberRepository GroupMemberRepository { get; }
    IFriendshipRepository FriendshipRepository { get; }
    IMessageRepository MessageRepository { get; }
    IPrivateConversationRepository ConversationRepository { get; }
    IStoryViewRepository StoryViewRepository { get; }
    IStoryRepository StoryRepository { get; }
    IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> SaveChangesAsync();
}
