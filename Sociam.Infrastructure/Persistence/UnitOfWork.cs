using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Interfaces;
using Sociam.Infrastructure.Persistence.Repositories;
using System.Collections;

namespace Sociam.Infrastructure.Persistence;
public sealed class UnitOfWork(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration) : IUnitOfWork
{
    private readonly Hashtable _repositories = [];
    public IFriendshipRepository FriendshipRepository => new FriendshipRepository(context, userManager);

    public IMessageRepository MessageRepository => new MessageRepository(context);

    public IPrivateConversationRepository ConversationRepository => new PrivateConversationRepository(context);
    public IStoryViewRepository StoryViewRepository => new StoryViewRepository(context, userManager);
    public IStoryRepository StoryRepository => new StoryRepository(context, configuration);

    public IGroupMemberRepository GroupMemberRepository => new GroupMemberRepository(context);

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

    public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;
        if (_repositories.ContainsKey(type)) return _repositories[type] as IGenericRepository<TEntity>;
        var repository = new GenericRepository<TEntity>(context);
        _repositories.Add(type, repository);

        return _repositories[type] as IGenericRepository<TEntity>;
    }

    public async ValueTask DisposeAsync() => await context.DisposeAsync();

}
