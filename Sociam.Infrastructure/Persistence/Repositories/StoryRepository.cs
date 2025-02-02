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

}