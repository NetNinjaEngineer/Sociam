using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories;

public sealed class StoryViewRepository(ApplicationDbContext context) : GenericRepository<StoryView>(context), IStoryViewRepository
{
    public async Task<bool> IsSetAsStoryViewerAsync(Guid storyId, string viewerId)
        => await context.StoryViews.AnyAsync(x => x.StoryId == storyId && x.ViewerId == viewerId && !x.IsViewed);
}