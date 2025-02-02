using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Commands.CreateMediaStory;
public sealed class CreateMediaStoryCommandHandler
    (IStoryService storyService) : IRequestHandler<CreateMediaStoryCommand, Result<MediaStoryDto>>
{
    public async Task<Result<MediaStoryDto>> Handle(CreateMediaStoryCommand request, CancellationToken cancellationToken)
        => await storyService.CreateMediaStoryAsync(request);
}
