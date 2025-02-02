using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Domain.Interfaces;
public interface IStoryRepository : IGenericRepository<Story>
{
    Task<IEnumerable<StoryDto>> GetActiveCreatorStoriesAsync(string creatorId);
    Task<bool> HasUnseenStoriesAsync(string currentUserId, string friendId);
    Task<UserWithStoriesDto?> GetActiveUserStoriesAsync(string currentUserId, string friendId);
}
