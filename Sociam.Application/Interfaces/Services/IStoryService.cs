using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;

namespace Sociam.Application.Interfaces.Services;
public interface IStoryService
{
    Task<Result<StoryDto>> CreateStoryAsync(CreateStoryCommand command);
}
