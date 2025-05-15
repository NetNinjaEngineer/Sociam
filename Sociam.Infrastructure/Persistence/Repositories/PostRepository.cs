using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories;

public sealed class PostRepository(ApplicationDbContext context)
    : GenericRepository<Post>(context), IPostRepository
{
    public async Task<bool> IsUserHasReactedToPostAsync(Guid postId, string userId)
        => await context.PostReactions
            .AsNoTracking()
            .AnyAsync(p => p.PostId == postId && p.ReactedById == userId);
}
