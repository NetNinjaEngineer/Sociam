using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sociam.Application.Helpers;
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
    public async Task<IEnumerable<StoryDto>> GetActiveCreatorStoriesAsync(
        string creatorId, string timeZoneId)
        => await context.Stories
            .AsNoTracking()
            .Where(s => s.UserId == creatorId && s.ExpiresAt > DateTimeOffset.UtcNow)
            .Select(s => new StoryDto
            {
                Id = s.Id,
                ExpiresAt = s.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId),
                CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
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
            .Where(story => story.UserId == friendId && story.ExpiresAt > DateTimeOffset.UtcNow)
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

    public async Task<UserWithStoriesDto?> GetActiveUserStoriesAsync(
        string currentUserId,
        string friendId,
        string timeZoneId)
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
                    .Where(s => s.ExpiresAt > DateTimeOffset.UtcNow &&
                                (s.StoryPrivacy == StoryPrivacy.Public ||
                                 (s.StoryPrivacy == StoryPrivacy.Friends && isFriend) ||
                                 (s.StoryPrivacy == StoryPrivacy.Custom && isSetAsStoryViewer)))
                    .OrderByDescending(s => s.CreatedAt)
                    .Select(s => new StoryForReturnDto
                    {
                        Id = s.Id,
                        CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
                        ExpiresAt = s.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId),
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

    public async Task<StoryViewsResponseDto?> GetStoryViewsAsync(
        Guid existedStoryId, string currentUserId, string timeZoneId)
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
                ExpiresAt = s.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId),
                CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
                ReactionsCount = s.StoryReactions.Count,
                CommentsCount = s.StoryComments.Count,
                ViewsCount = s.StoryViewers.Count,
                Viewers = s.StoryViewers
                    .Where(v => v.IsViewed)
                    .Select(view => new StoryViewerDto
                    {
                        ViewerId = view.ViewerId,
                        ViewerName = string.Concat(view.Viewer.FirstName, " ", view.Viewer.LastName),
                        ViewedAt = view.ViewedAt.HasValue ? view.ViewedAt.Value.ConvertToUserLocalTimeZone(timeZoneId) : null,
                        ProfilePictureUrl = string.IsNullOrEmpty(s.User.ProfilePictureUrl) ? null : $"{apiBaseUrl}/Uploads/Images/{view.Viewer.ProfilePictureUrl}",
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        return storyViews;
    }

    public async Task<List<StoryViewedDto>> GetStoriesViewedByMeAsync(
        string currentUserId, string timeZoneId)
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
                CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
                ReactionsCount = s.StoryReactions.Count,
                CommentsCount = s.StoryComments.Count,
                ViewsCount = s.StoryViewers.Count
            }).ToListAsync();

    public async Task<PagedResult<StoryViewsResponseDto>> GetStoriesWithParamsForMeAsync(
        string currentUserId,
        string timeZoneId,
        StoryQueryParameters? queryStoryQueryParameters) => await GetStoriesAsync(timeZoneId, s => s.UserId == currentUserId, queryStoryQueryParameters);

    public async Task<PagedResult<StoryViewsResponseDto>> GetExpiredStoriesAsync(
        string creatorId,
        string timeZoneId,
        StoryQueryParameters? queryParameters)
    {
        // Get the user's time zone
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

        // Calculate the current time in UTC
        var currentTimeUtc = DateTimeOffset.UtcNow;

        // Calculate the UTC offset for the user's time zone
        var utcOffset = timeZone.GetUtcOffset(currentTimeUtc.DateTime);

        var pagedResult = await GetStoriesAsync(timeZoneId,
            s => s.UserId == creatorId && s.ExpiresAt <= currentTimeUtc + utcOffset, queryParameters);

        return pagedResult;

    }

    public async Task<PagedResult<StoryViewsResponseDto>> GetStoryArchiveAsync(
        string currentUserId,
        string timeZoneId,
        StoryQueryParameters? queryParameters)
        => await GetStoriesAsync(timeZoneId, s => s.UserId == currentUserId && s.IsArchived, queryParameters);

    public async Task<string?> GetStoryOwnerIdAsync(Guid storyId)
        => (await context.Stories
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == storyId))?.UserId;

    public async Task<IEnumerable<StoryWithCommentsResponseDto>> GetAllStoriesWithCommentsAsync(
        string currentUserId, string timeZoneId)
        => await context.Stories
            .AsNoTracking()
            .Where(s => s.UserId == currentUserId)
            .Select(s => new StoryWithCommentsResponseDto
            {
                Id = s.Id,
                Content = s is TextStory ? ((TextStory)s).Content : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
                ExpiredAt = s.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId),
                HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                MediaType = s is MediaStory ? ((MediaStory)s).MediaType.ToString() : null,
                MediaUrl = s is MediaStory
                    ? IsVideoMediaType(((MediaStory)s).MediaType)
                        ? $"{configuration["BaseApiUrl"]}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}"
                        : $"{configuration["BaseApiUrl"]}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}"
                    : null,
                Privacy = s.StoryPrivacy.ToString(),
                Type = s is TextStory ? "text" : "media",
                IsArchived = s.IsArchived,
                Commenters =
                    s.StoryComments
                        .Select(comment => new StoryCommenterResponseDto()
                        {
                            FirstName = comment.CommentedBy.FirstName ?? "",
                            LastName = comment.CommentedBy.LastName ?? "",
                            ProfilePictureUrl = comment.CommentedBy.ProfilePictureUrl != null
                                ? $"{configuration["BaseApiUrl"]}/Uploads/Images/{comment.CommentedBy.ProfilePictureUrl}"
                                : null,
                            Comment = comment.Comment,
                            UserName = comment.CommentedBy.UserName ?? "",
                            CommenterId = comment.CommentedById.ToString(),
                            CommentedAt = comment.CommentedAt.ConvertToUserLocalTimeZone(timeZoneId)
                        })
            }).ToListAsync();

    public async Task<StoryWithCommentsResponseDto?> GetStoryWithCommentsAsync(
        string currentUserId, Guid queryStoryId, string timeZoneId)
        => await context.Stories
            .AsNoTracking()
            .Where(s => s.UserId == currentUserId && s.Id == queryStoryId)
            .Select(s => new StoryWithCommentsResponseDto()
            {
                Id = s.Id,
                Content = s is TextStory ? ((TextStory)s).Content : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                ExpiredAt = s.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId),
                CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
                HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                MediaType = s is MediaStory ? ((MediaStory)s).MediaType.ToString() : null,
                MediaUrl = s is MediaStory
                    ? IsVideoMediaType(((MediaStory)s).MediaType)
                        ? $"{configuration["BaseApiUrl"]}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}"
                        : $"{configuration["BaseApiUrl"]}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}"
                    : null,
                Privacy = s.StoryPrivacy.ToString(),
                Type = s is TextStory ? "text" : "media",
                IsArchived = s.IsArchived,
                Commenters =
                    s.StoryComments
                        .Select(comment => new StoryCommenterResponseDto()
                        {
                            FirstName = comment.CommentedBy.FirstName ?? "",
                            LastName = comment.CommentedBy.LastName ?? "",
                            ProfilePictureUrl = comment.CommentedBy.ProfilePictureUrl != null
                                ? $"{configuration["BaseApiUrl"]}/Uploads/Images/{comment.CommentedBy.ProfilePictureUrl}"
                                : null,
                            Comment = comment.Comment,
                            UserName = comment.CommentedBy.UserName ?? "",
                            CommenterId = comment.CommentedById.ToString(),
                            CommentedAt = comment.CommentedAt.ConvertToUserLocalTimeZone(timeZoneId)
                        })
            }).FirstOrDefaultAsync();

    public async Task<StoryWithReactionsResponseDto?> GetStoryWithReactionsAsync(
        string currentUserId, Guid queryStoryId, string timeZoneId)
        => await context.Stories
            .AsNoTracking()
            .Where(s => s.UserId == currentUserId && s.Id == queryStoryId)
            .Select(s => new StoryWithReactionsResponseDto
            {
                Id = s.Id,
                Content = s is TextStory ? ((TextStory)s).Content : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                ExpiredAt = s.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId),
                CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
                HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                MediaType = s is MediaStory ? ((MediaStory)s).MediaType.ToString() : null,
                MediaUrl = s is MediaStory
                    ? IsVideoMediaType(((MediaStory)s).MediaType)
                        ? $"{configuration["BaseApiUrl"]}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}"
                        : $"{configuration["BaseApiUrl"]}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}"
                    : null,
                Privacy = s.StoryPrivacy.ToString(),
                Type = s is TextStory ? "text" : "media",
                IsArchived = s.IsArchived,
                Reactions =
                    s.StoryReactions
                        .Select(reaction => new StoryReactionResponseDto
                        {
                            ReactedBy = string.Concat(reaction.ReactedBy.FirstName, " ", reaction.ReactedBy.LastName),
                            ProfilePictureUrl = reaction.ReactedBy.ProfilePictureUrl != null
                                ? $"{configuration["BaseApiUrl"]}/Uploads/Images/{reaction.ReactedBy.ProfilePictureUrl}"
                                : null,
                            ReactionType = reaction.ReactionType.ToString(),
                            ReactedAt = reaction.ReactedAt.ConvertToUserLocalTimeZone(timeZoneId),
                            ReactedById = reaction.ReactedById
                        })
            }).FirstOrDefaultAsync();

    public async Task<StoryStatisticsDto?> GetStoryStatisticsAsync(
        Guid queryStoryId, string currentUserId, string timeZoneId)
    {
        return await context.Stories
            .AsNoTracking()
            .Include(s => s.StoryComments)
            .Include(s => s.StoryReactions)
            .Include(s => s.StoryViewers)
            .ThenInclude(sv => sv.Viewer)
            .Where(s => s.UserId == currentUserId && s.Id == queryStoryId)
            .Select(s => new StoryStatisticsDto
            {
                StoryId = s.Id,
                Content = s is TextStory ? ((TextStory)s).Content : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                ExpiresAt = s.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId),
                CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
                HashTags = s is TextStory ? ((TextStory)s).HashTags : null,
                MediaType = s is MediaStory ? ((MediaStory)s).MediaType.ToString() : null,
                MediaUrl = s is MediaStory
                    ? IsVideoMediaType(((MediaStory)s).MediaType)
                        ? $"{configuration["BaseApiUrl"]}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}"
                        : $"{configuration["BaseApiUrl"]}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}"
                    : null,
                Privacy = s.StoryPrivacy.ToString(),
                Type = s is TextStory ? "text" : "media",
                IsArchived = s.IsArchived,
                Reactions =
                    s.StoryReactions
                        .Select(reaction => new StoryReactionResponseDto
                        {
                            ReactedBy = string.Concat(reaction.ReactedBy.FirstName, " ", reaction.ReactedBy.LastName),
                            ProfilePictureUrl = reaction.ReactedBy.ProfilePictureUrl != null
                                ? $"{configuration["BaseApiUrl"]}/Uploads/Images/{reaction.ReactedBy.ProfilePictureUrl}"
                                : null,
                            ReactionType = reaction.ReactionType.ToString(),
                            ReactedAt = reaction.ReactedAt.ConvertToUserLocalTimeZone(timeZoneId),
                            ReactedById = reaction.ReactedById
                        }).ToList(),
                TotalComments = s.StoryComments.Count,
                TotalViews = s.StoryViewers.Count,
                Comments = s.StoryComments
                    .Select(comment => new StoryCommenterResponseDto()
                    {
                        FirstName = comment.CommentedBy.FirstName ?? "",
                        LastName = comment.CommentedBy.LastName ?? "",
                        ProfilePictureUrl = comment.CommentedBy.ProfilePictureUrl != null
                            ? $"{configuration["BaseApiUrl"]}/Uploads/Images/{comment.CommentedBy.ProfilePictureUrl}"
                            : null,
                        Comment = comment.Comment,
                        UserName = comment.CommentedBy.UserName ?? "",
                        CommenterId = comment.CommentedById.ToString(),
                        CommentedAt = comment.CommentedAt.ConvertToUserLocalTimeZone(timeZoneId)
                    }).ToList(),
                UniqueViews = s.StoryViewers.Distinct().Count(),
                Viewers = s.StoryViewers
                    .Where(view => view.IsViewed)
                    .Select(view => new ViewerBreakdownDto
                    {
                        ViewedAt = view.ViewedAt.HasValue ? view.ViewedAt.Value.ConvertToUserLocalTimeZone(timeZoneId) : null,
                        ViewerName = $"{view.Viewer.FirstName} {view.Viewer.LastName}",
                        ProfilePictureUrl = string.IsNullOrEmpty(view.Viewer.ProfilePictureUrl)
                            ? null : $"{configuration["BaseApiUrl"]}/Uploads/Images/{view.Viewer.ProfilePictureUrl}",
                        ViewerId = view.ViewerId,
                        ViewerType = ""
                    }).ToList(),
                TotalReactions = s.StoryReactions.Count,
                ViewersByAgeGroup = GetViewersByAgeGroup(s.StoryViewers),
                ReactionsBreakdown = GetReactionBreakdown(s.StoryReactions),
                GetViewersByGender = GetViewersByGender(s.StoryViewers),
                UserReactions =
                    s.StoryReactions
                    .GroupBy(sr => sr.ReactedById)
                    .Select(g =>
                        new UserReactionDto
                        {
                            User = g.Select(x => string.Concat(x.ReactedBy.FirstName, " ", x.ReactedBy.LastName)).FirstOrDefault() ?? "",
                            Reactions = g.Select(x => x.ReactionType.ToString()).ToList()
                        }).ToList()

            }).FirstOrDefaultAsync();
    }

    private static Dictionary<string, int> GetViewersByGender(ICollection<StoryView> storyViewers)
        => storyViewers.GroupBy(x => x.Viewer.Gender)
            .ToDictionary(
                x => x.Key.ToString(),
                x => x.Count());

    private static Dictionary<string, int> GetReactionBreakdown(ICollection<StoryReaction>? reactions)
    {
        if (reactions == null || reactions.Count == 0)
            return new Dictionary<string, int>();

        return reactions
            .GroupBy(r => r.ReactionType)
            .ToDictionary(g => g.Key.ToString(), g => g.Count());
    }

    private static Dictionary<string, int> GetViewersByAgeGroup(ICollection<StoryView> storyViewers)
    {
        return storyViewers.GroupBy(v => CalculateAgeGroup(v.Viewer.DateOfBirth))
            .ToDictionary(x => x.Key, x => x.Count());
    }

    private static string CalculateAgeGroup(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth.AddYears(age) > today)
            age--;

        return age switch
        {
            < 13 => "Under 13",
            < 18 => "13-17",
            < 25 => "18-24",
            < 35 => "25-34",
            < 45 => "35-44",
            < 55 => "45-54",
            < 65 => "55-64",
            _ => "65+"
        };
    }

    private async Task<PagedResult<StoryViewsResponseDto>> GetStoriesAsync(
        string timeZoneId,
        Expression<Func<Story, bool>>? predicate,
        StoryQueryParameters? queryStoryQueryParameters)
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
                        ? $"{configuration["BaseApiUrl"]}/Uploads/Stories/Videos/{((MediaStory)s).MediaUrl}"
                        : $"{configuration["BaseApiUrl"]}/Uploads/Stories/Images/{((MediaStory)s).MediaUrl}"
                    : null,
                Caption = s is MediaStory ? ((MediaStory)s).Caption : null,
                ExpiresAt = s.ExpiresAt.ConvertToUserLocalTimeZone(timeZoneId),
                CreatedAt = s.CreatedAt.ConvertToUserLocalTimeZone(timeZoneId),
                ReactionsCount = s.StoryReactions.Count,
                CommentsCount = s.StoryComments.Count,
                ViewsCount = s.StoryViewers.Count,
                Viewers = s.StoryViewers
                    .Select(view => new StoryViewerDto
                    {
                        ViewedAt = view.ViewedAt.HasValue ? view.ViewedAt.Value.ConvertToUserLocalTimeZone(timeZoneId) : null,
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