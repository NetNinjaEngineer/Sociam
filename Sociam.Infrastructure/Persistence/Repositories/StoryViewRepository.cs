using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Infrastructure.Persistence.Repositories;

public sealed class StoryViewRepository(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager) : GenericRepository<StoryView>(context), IStoryViewRepository
{
    public async Task<bool> IsSetAsStoryViewerAsync(Guid storyId, string viewerId)
        => await context.StoryViews.AnyAsync(x => x.StoryId == storyId && x.ViewerId == viewerId && !x.IsViewed);

    public async Task<long> GetTotalViewsForStoryAsync(Guid activeStoryId)
        => await context.StoryViews.Where(x => x.StoryId == activeStoryId).LongCountAsync();

    public async Task<IEnumerable<StoryViewsDto>?> GetStoryViewersAsync(Guid activeStoryId)
    {
        var viewersIds = context.StoryViews
            .AsNoTracking()
            .Where(x => x.StoryId == activeStoryId)
            .Select(x => x.ViewerId)
            .ToList();

        var users = await userManager.Users
            .AsNoTracking()
            .Where(u => viewersIds.Contains(u.Id))
            .ToListAsync();

        var views = users.Select(x => new StoryViewsDto
        {
            UserId = x.Id,
            ViewerName = string.Concat(x.FirstName, " ", x.LastName),
            ProfilePictureUrl = x.ProfilePictureUrl ?? "",
            StoryId = activeStoryId,
            ViewedAt = context.StoryViews.FirstOrDefault(sv =>
                sv.StoryId == activeStoryId && sv.ViewerId == x.Id)?.ViewedAt
        });

        return views;
    }

    public async Task<bool> IsStoryViewedAsync(Guid activeStoryId, string currentUserId)
        => await context.StoryViews.AsNoTracking()
            .AnyAsync(sv => sv.StoryId == activeStoryId && sv.ViewerId == currentUserId && sv.IsViewed);

    public async Task<IEnumerable<StoryView>> GetAllowedUsersAsync(Guid storyId)
    {
        var allowedUsers = await context.StoryViews
            .AsNoTracking()
            .Include(storyView => storyView.Viewer)
            .Include(storyView => storyView.Story)
            .Where(storyView => storyView.StoryId == storyId && !storyView.IsViewed && storyView.ViewedAt == null)
            .ToListAsync();

        return allowedUsers;
    }
}