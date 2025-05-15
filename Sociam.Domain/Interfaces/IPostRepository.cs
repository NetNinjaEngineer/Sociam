using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;

public interface IPostRepository : IGenericRepository<Post>
{
    Task<bool> IsUserHasReactedToPostAsync(Guid postId, string userId);
}
