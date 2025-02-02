using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
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

    public async Task<UserWithStoriesDto?> GetActiveUserStoriesAsync(string currentUserId, string friendId)
    {
        var isFriend = await context.Friendships
            .AsNoTracking()
            .Include(x => x.Requester)
            .Include(x => x.Receiver)
            .AnyAsync(x => (x.RequesterId == currentUserId && x.ReceiverId == friendId) ||
                           (x.RequesterId == friendId && x.ReceiverId == currentUserId) &&
                           x.FriendshipStatus == FriendshipStatus.Accepted);

        var isSetAsStoryViewer = await context.StoryViews
            .AsNoTracking()
            .AnyAsync(x => !x.IsViewed && x.ViewerId == currentUserId);

        var userWithStories = await context.Users
            .Where(u => u.Id == friendId && isFriend)
            .Select(u => new UserWithStoriesDto
            {
                UserId = u.Id,
                FirstName = u.FirstName ?? "",
                LastName = u.LastName ?? "",
                FullName = string.Concat(u.FirstName, " ", u.LastName),
                UserName = u.UserName ?? "",
                ProfilePicture = u.ProfilePictureUrl,
                Stories = u.Stories
                    .Where(s => s.ExpiresAt > DateTimeOffset.Now &&
                                (s.StoryPrivacy == StoryPrivacy.Public ||
                                 (s.StoryPrivacy == StoryPrivacy.Friends && isFriend) ||
                                 (s.StoryPrivacy == StoryPrivacy.Custom && isSetAsStoryViewer)))
                    .OrderByDescending(s => s.CreatedAt)
                    .Select(s => new StoryForReturnDto
                    {
                        Id = s.Id,
                        CreatedAt = s.CreatedAt,
                        ExpiresAt = s.ExpiresAt,
                        StoryPrivacy = s.StoryPrivacy,
                        HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                        Content = s is TextStory ? ((TextStory)s).Content : null,
                        MediaType = s is MediaStory ? ((MediaStory)s).MediaType : null,
                        MediaUrl = s is MediaStory ? ((MediaStory)s).MediaUrl : null,
                        Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                        ViewersCount = s.StoryViewers.Count,
                        ReactionsCount = s.StoryReactions.Count,
                        CommentsCount = s.StoryComments.Count
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return userWithStories;
    }
}