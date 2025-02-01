using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Domain.Interfaces;

public interface IStoryViewRepository : IGenericRepository<StoryView>
{
    Task<bool> IsSetAsStoryViewerAsync(Guid storyId, string viewerId);
    Task<long> GetTotalViewsForStoryAsync(Guid activeStoryId);
    Task<IEnumerable<StoryViewsDto>?> GetStoryViewersAsync(Guid activeStoryId);
}