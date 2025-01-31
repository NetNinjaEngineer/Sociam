using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;

namespace Sociam.Application.Interfaces.Services;
public interface IStoryService
{
    Task<Result<StoryDto>> CreateStoryAsync(CreateStoryCommand command);
    Task<Result<IEnumerable<StoryDto>>> GetActiveFriendStoriesAsync(GetActiveFriendStoriesQuery query);
}
