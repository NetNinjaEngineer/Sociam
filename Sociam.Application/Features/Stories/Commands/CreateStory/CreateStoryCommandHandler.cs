using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Commands.CreateStory;
public sealed class CreateStoryCommandHandler
    (IStoryService storyService) : IRequestHandler<CreateStoryCommand, Result<StoryDto>>
{
    public async Task<Result<StoryDto>> Handle(CreateStoryCommand request, CancellationToken cancellationToken)
        => await storyService.CreateStoryAsync(request);
}
