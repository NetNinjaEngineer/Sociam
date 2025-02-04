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
    Task<List<StoryViewsResponseDto>> GetStoriesWithParamsForMeAsync(string currentUserId, StoryQueryParameters queryStoryQueryParameters);
    Task<IEnumerable<StoryViewsResponseDto>> GetExpiredStoriesAsync(string creatorId);
}
