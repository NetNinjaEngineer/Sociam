using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;
using System.Linq.Expressions;

namespace Sociam.Infrastructure.Persistence.Repositories;

public sealed class StoryRepository(
    ApplicationDbContext context,
    IConfiguration configuration)
    : GenericRepository<Story>(context), IStoryRepository
{
    public async Task<IEnumerable<StoryDto>> GetActiveCreatorStoriesAsync(string creatorId)
        => await context.Stories
            .AsNoTracking()
            .Where(s => s.UserId == creatorId && s.ExpiresAt > DateTimeOffset.Now)
            .Select(s => new StoryDto
            {
                Id = s.Id,
                ExpiresAt = s.ExpiresAt,
                CreatedAt = s.CreatedAt,
                StoryPrivacy = s.StoryPrivacy,
                UserId = s.UserId,
                StoryType = s is TextStory ? "text" : "media",
                HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                Content = s is TextStory ? ((TextStory)s).Content : null,
                MediaType = s is MediaStory ? ((MediaStory)s).MediaType : null,
                MediaUrl = s is MediaStory ? (IsVideoMediaType(((MediaStory)s).MediaType) ?
                    $"{configuration["BaseApiUrl"]}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}" :
                    $"{configuration["BaseApiUrl"]}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}") : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null
            })
            .ToListAsync();

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
        var apiBaseUrl = configuration["BaseApiUrl"];


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
                        MediaUrl = s is MediaStory ? (IsVideoMediaType(((MediaStory)s).MediaType) ?
                            $"{apiBaseUrl}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}" :
                            $"{apiBaseUrl}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}") : null,
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

    public async Task<StoryViewsResponseDto?> GetStoryViewsAsync(Guid existedStoryId, string currentUserId)
    {
        var apiBaseUrl = configuration["BaseApiUrl"];

        var storyViews = await context.Stories
            .AsNoTracking()
            .Where(s => s.Id == existedStoryId && s.UserId == currentUserId)
            .Select(s => new StoryViewsResponseDto
            {
                StoryId = s.Id,
                CreatorId = s.UserId,
                CreatorFirstName = s.User.FirstName ?? "",
                CreatorLastName = s.User.LastName ?? "",
                CreatorUserName = s.User.UserName ?? "",
                CreatorProfilePicture = string.IsNullOrEmpty(s.User.ProfilePictureUrl) ? null : $"{apiBaseUrl}/Uploads/Images/{s.User.ProfilePictureUrl}",
                HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                Content = s is TextStory ? ((TextStory)s).Content : null,
                MediaType = s is MediaStory ? ((MediaStory)s).MediaType : null,
                MediaUrl = s is MediaStory ? (IsVideoMediaType(((MediaStory)s).MediaType) ?
                    $"{apiBaseUrl}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}" :
                    $"{apiBaseUrl}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}") : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                ExpiresAt = s.ExpiresAt,
                CreatedAt = s.CreatedAt,
                ReactionsCount = s.StoryReactions.Count,
                CommentsCount = s.StoryComments.Count,
                ViewsCount = s.StoryViewers.Count,
                Viewers = s.StoryViewers
                    .Where(v => v.IsViewed)
                    .Select(view => new StoryViewerDto
                    {
                        ViewerId = view.ViewerId,
                        ViewerName = string.Concat(view.Viewer.FirstName, " ", view.Viewer.LastName),
                        ViewedAt = view.ViewedAt,
                        ProfilePictureUrl = string.IsNullOrEmpty(s.User.ProfilePictureUrl) ? null : $"{apiBaseUrl}/Uploads/Images/{view.Viewer.ProfilePictureUrl}",
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        return storyViews;
    }

    public async Task<List<StoryViewedDto>> GetStoriesViewedByMeAsync(string currentUserId)
        => await context.Stories
            .AsNoTracking()
            .Where(s => s.StoryViewers.Any(v => v.ViewerId == currentUserId && v.IsViewed))
            .Select(s => new StoryViewedDto
            {
                StoryId = s.Id,
                CreatorId = s.UserId,
                CreatorFullName = "",
                CreatorProfilePicture = string.IsNullOrEmpty(s.User.ProfilePictureUrl)
                    ? null
                    : $"{configuration["BaseApiUrl"]}/Uploads/Images/{s.User.ProfilePictureUrl}",
                HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                Content = s is TextStory ? ((TextStory)s).Content : null,
                MediaType = s is MediaStory ? ((MediaStory)s).MediaType : null,
                MediaUrl = s is MediaStory ?
                    (IsVideoMediaType(((MediaStory)s).MediaType) ?
                        $"{configuration["BaseApiUrl"]}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}" :
                        $"{configuration["BaseApiUrl"]}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}") : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                CreatedAt = s.CreatedAt,
                ReactionsCount = s.StoryReactions.Count,
                CommentsCount = s.StoryComments.Count,
                ViewsCount = s.StoryViewers.Count
            }).ToListAsync();

    public async Task<PagedResult<StoryViewsResponseDto>> GetStoriesWithParamsForMeAsync(
        string currentUserId,
        StoryQueryParameters? queryStoryQueryParameters) => await GetStoriesAsync(null, queryStoryQueryParameters);

    public async Task<PagedResult<StoryViewsResponseDto>> GetExpiredStoriesAsync(
        string creatorId,
        StoryQueryParameters? queryParameters)
     => await GetStoriesAsync(s => s.UserId == creatorId && s.ExpiresAt < DateTimeOffset.Now, queryParameters);

    public async Task<PagedResult<StoryViewsResponseDto>> GetStoryArchiveAsync(
        string currentUserId,
        StoryQueryParameters? queryParameters)
        => await GetStoriesAsync(s => s.UserId == currentUserId && s.IsArchived, queryParameters);

    public async Task<string?> GetStoryOwnerIdAsync(Guid storyId)
        => (await context.Stories
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == storyId))?.UserId;

    private async Task<PagedResult<StoryViewsResponseDto>> GetStoriesAsync(
        Expression<Func<Story, bool>>? predicate, StoryQueryParameters? queryStoryQueryParameters)
    {
        var storiesQuery = predicate is null ?
           context.Stories.AsQueryable() : context.Stories.Where(predicate).AsQueryable();

        if (queryStoryQueryParameters is { StartDate: not null })
            storiesQuery = storiesQuery.Where(s => DateOnly.FromDateTime(s.CreatedAt.Date) >= queryStoryQueryParameters.StartDate);

        if (queryStoryQueryParameters is { EndDate: not null })
            storiesQuery = storiesQuery.Where(s =>
                DateOnly.FromDateTime(s.CreatedAt.Date) <= queryStoryQueryParameters.EndDate);

        if (!string.IsNullOrEmpty(queryStoryQueryParameters?.Contains))
            storiesQuery = storiesQuery.Where(s =>
                (s is TextStory && ((TextStory)s).Content!.Contains(queryStoryQueryParameters.Contains)) ||
                (s is MediaStory && ((MediaStory)s).Caption!.Contains(queryStoryQueryParameters.Contains)));

        if (queryStoryQueryParameters is { MediaType: not null })
            storiesQuery = storiesQuery.Where(s =>
                (s is MediaStory && ((MediaStory)s).MediaType == queryStoryQueryParameters.MediaType));

        if (queryStoryQueryParameters is { Privacy: not null })
            storiesQuery = storiesQuery.Where(s => s.StoryPrivacy == queryStoryQueryParameters.Privacy);

        var stories = await storiesQuery.Select(
            s => new StoryViewsResponseDto
            {
                StoryId = s.Id,
                CreatorId = s.UserId,
                CreatorFirstName = s.User.FirstName ?? "",
                CreatorLastName = s.User.LastName ?? "",
                CreatorProfilePicture = string.IsNullOrEmpty(s.User.ProfilePictureUrl)
                    ? null
                    : $"{configuration["BaseApiUrl"]}/Uploads/Images/{s.User.ProfilePictureUrl}",
                CreatorUserName = s.User.UserName ?? "",
                HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                Content = s is TextStory ? ((TextStory)s).Content : null,
                MediaType = s is MediaStory ? ((MediaStory)s).MediaType : null,
                MediaUrl = s is MediaStory
                    ? IsVideoMediaType(((MediaStory)s).MediaType)
                        ? $"{configuration["BaseApiUrl"]}/Uploads/Stories/Videos/{s.User.ProfilePictureUrl}"
                        : $"{configuration["BaseApiUrl"]}/Uploads/Stories/Images/{s.User.ProfilePictureUrl}"
                    : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                ExpiresAt = s.ExpiresAt,
                CreatedAt = s.CreatedAt,
                ReactionsCount = s.StoryReactions.Count,
                CommentsCount = s.StoryComments.Count,
                ViewsCount = s.StoryViewers.Count,
                Viewers = s.StoryViewers
                    .Select(view => new StoryViewerDto
                    {
                        ViewedAt = view.ViewedAt,
                        ViewerName = $"{view.Viewer.FirstName} {view.Viewer.LastName}",
                        ProfilePictureUrl = view.Viewer.ProfilePictureUrl,
                    }).ToList()
            }).ToListAsync();

        if (queryStoryQueryParameters?.Hashtags?.Count > 0)
            stories = stories
                .Where(s => s.HashTags != null &&
                            s.HashTags.Any(ht => queryStoryQueryParameters.Hashtags.Contains(ht)))
                .ToList();

        var totalCount = await storiesQuery.CountAsync();

        if (queryStoryQueryParameters != null)
            stories = stories.Skip((queryStoryQueryParameters.PageNumber - 1) * queryStoryQueryParameters.PageSize)
                .Take(queryStoryQueryParameters.PageSize)
                .ToList();

        return new PagedResult<StoryViewsResponseDto>
        {
            Page = queryStoryQueryParameters?.PageNumber,
            PageSize = queryStoryQueryParameters?.PageSize,
            Items = stories,
            TotalCount = totalCount
        };
    }

    private static bool IsVideoMediaType(MediaType? mediaType) => mediaType == MediaType.Video;
}