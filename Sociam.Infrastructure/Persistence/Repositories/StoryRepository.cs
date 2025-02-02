using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Infrastructure.Persistence.Repositories;

public sealed class StoryRepository(ApplicationDbContext context) : GenericRepository<Story>(context), IStoryRepository
{
    public async Task<IEnumerable<StoryDto>> GetActiveCreatorStoriesAsync(string creatorId)
    {
        var stories = await context.Stories
            .AsNoTracking()
            .Where(s => s.UserId == creatorId && s.ExpiresAt > DateTimeOffset.Now)
            .ToListAsync();

        return stories.Select(
            s => new StoryDto
            {
                Id = s.Id,
                ExpiresAt = s.ExpiresAt,
                CreatedAt = s.CreatedAt,
                StoryPrivacy = s.StoryPrivacy,
                UserId = s.UserId,
                HashTags = s is TextStory textStory ? textStory.HashTags : null,
                MediaUrl = s is MediaStory mediaStory ? mediaStory.MediaUrl : null,
                MediaType = s is MediaStory mediaStoryType ? mediaStoryType.MediaType : null
            }).ToList();
    }

    public async Task<bool> HasUnseenStoriesAsync(string currentUserId, string friendId)
    {
        // Get active stories for the friend
        var activeFriendStories = await context.Stories
            .AsNoTracking()
            .Where(story => story.UserId == friendId && story.ExpiresAt > DateTimeOffset.Now)
            .ToListAsync();

        if (activeFriendStories.Count == 0) return false;

        // Get the stories already viewed by me
        var viewedStoriesIds = await context.StoryViews
            .AsNoTracking()
            .Where(storyView => storyView.IsViewed && storyView.ViewerId == currentUserId)
            .Select(storyView => storyView.StoryId)
            .ToListAsync();

        return activeFriendStories.Any(activeStory => !viewedStoriesIds.Contains(activeStory.Id));
    }
}