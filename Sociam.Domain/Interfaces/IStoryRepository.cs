using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Interfaces;
public interface IStoryRepository : IGenericRepository<Story>
{
    Task<IEnumerable<StoryDto>> GetActiveCreatorStoriesAsync(string creatorId, string timeZoneId);
    Task<bool> HasUnseenStoriesAsync(string currentUserId, string friendId);
    Task<UserWithStoriesDto?> GetActiveUserStoriesAsync(string currentUserId, string friendId, string timeZoneId);
    Task<StoryViewsResponseDto?> GetStoryViewsAsync(Guid existedStoryId, string currentUserId, string timeZoneId);
    Task<List<StoryViewedDto>> GetStoriesViewedByMeAsync(string currentUserId, string timeZoneId);
    Task<PagedResult<StoryViewsResponseDto>> GetStoriesWithParamsForMeAsync(string currentUserId, string timeZoneId, StoryQueryParameters? queryStoryQueryParameters);
    Task<PagedResult<StoryViewsResponseDto>> GetExpiredStoriesAsync(string creatorId, string timeZoneId, StoryQueryParameters? queryParameters);
    Task<PagedResult<StoryViewsResponseDto>> GetStoryArchiveAsync(string currentUserId, string timeZoneId, StoryQueryParameters? queryParameters);
    Task<string?> GetStoryOwnerIdAsync(Guid storyId);
    Task<IEnumerable<StoryWithCommentsResponseDto>> GetAllStoriesWithCommentsAsync(string currentUserId, string timeZoneId);
    Task<StoryWithCommentsResponseDto?> GetStoryWithCommentsAsync(string currentUserId, Guid queryStoryId, string timeZoneId);
    Task<StoryWithReactionsResponseDto?> GetStoryWithReactionsAsync(string currentUserId, Guid queryStoryId, string timeZoneId);
    Task<StoryStatisticsDto?> GetStoryStatisticsAsync(Guid queryStoryId, string currentUserId, string timeZoneId);
}
