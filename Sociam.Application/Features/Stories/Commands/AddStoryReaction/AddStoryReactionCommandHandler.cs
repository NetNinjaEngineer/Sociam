using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Commands.AddStoryReaction;
public sealed class AddStoryReactionCommandHandler(IStoryService service)
    : IRequestHandler<AddStoryReactionCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        AddStoryReactionCommand request, CancellationToken cancellationToken)
        => await service.ReactToStoryAsync(request);
}
