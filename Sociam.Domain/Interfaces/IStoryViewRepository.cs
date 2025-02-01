using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;

public interface IStoryViewRepository : IGenericRepository<StoryView>
{
    Task<bool> IsSetAsStoryViewerAsync(Guid storyId, string viewerId);
}