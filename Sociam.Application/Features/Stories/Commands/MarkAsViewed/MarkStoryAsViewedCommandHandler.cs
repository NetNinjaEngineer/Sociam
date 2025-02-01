using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Commands.MarkAsViewed;
public sealed class MarkStoryAsViewedCommandHandler(IStoryService service)
    : IRequestHandler<MarkStoryAsViewedCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        MarkStoryAsViewedCommand request, CancellationToken cancellationToken)
        => await service.MarkStoryAsViewedAsync(request);
}
