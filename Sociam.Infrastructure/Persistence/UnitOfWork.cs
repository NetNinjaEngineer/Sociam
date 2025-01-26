using Microsoft.AspNetCore.Identity;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Interfaces;
using Sociam.Infrastructure.Persistence.Repositories;
using System.Collections;

namespace Sociam.Infrastructure.Persistence;
public sealed class UnitOfWork(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager) : IUnitOfWork
{
    private readonly Hashtable _repositories = [];
    public IFriendshipRepository FriendshipRepository => new FriendshipRepository(context, userManager);

    public IMessageRepository MessageRepository => new MessageRepository(context);

    public IPrivateConversationRepository ConversationRepository => new PrivateConversationRepository(context);

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
