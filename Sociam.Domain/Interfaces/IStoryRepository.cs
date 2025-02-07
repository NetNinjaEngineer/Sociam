using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Interfaces;
public interface IStoryRepository : IGenericRepository<Story>
{
    Task<IEnumerable<StoryDto>> GetActiveCreatorStoriesAsync(string creatorId);
    Task<bool> HasUnseenStoriesAsync(string currentUserId, string friendId);
    Task<UserWithStoriesDto?> GetActiveUserStoriesAsync(string currentUserId, string friendId);
    Task<StoryViewsResponseDto?> GetStoryViewsAsync(Guid existedStoryId, string currentUserId);
    Task<List<StoryViewedDto>> GetStoriesViewedByMeAsync(string currentUserId);
    Task<PagedResult<StoryViewsResponseDto>> GetStoriesWithParamsForMeAsync(string currentUserId, StoryQueryParameters? queryStoryQueryParameters);
    Task<PagedResult<StoryViewsResponseDto>> GetExpiredStoriesAsync(string creatorId, StoryQueryParameters? queryParameters);
    Task<PagedResult<StoryViewsResponseDto>> GetStoryArchiveAsync(string currentUserId, StoryQueryParameters? queryParameters);
    Task<string?> GetStoryOwnerIdAsync(Guid storyId);
    Task<IEnumerable<StoryWithCommentsResponseDto>> GetAllStoriesWithCommentsAsync(string currentUserId);
    Task<StoryWithCommentsResponseDto?> GetStoryWithCommentsAsync(string currentUserId, Guid queryStoryId);
    Task<StoryWithReactionsResponseDto?> GetStoryWithReactionsAsync(string currentUserId, Guid queryStoryId);
}
