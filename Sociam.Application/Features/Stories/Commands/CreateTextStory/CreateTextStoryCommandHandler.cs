using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Commands.CreateTextStory;
public sealed class CreateTextStoryCommandHandler(IStoryService service)
    : IRequestHandler<CreateTextStoryCommand, Result<TextStoryDto>>
{
    public async Task<Result<TextStoryDto>> Handle(
        CreateTextStoryCommand request, CancellationToken cancellationToken)
        => await service.CreateTextStoryAsync(request);
}
