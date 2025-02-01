using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Commands.DeleteStory;
public sealed class DeleteStoryCommandHandler(
    IStoryService service) : IRequestHandler<DeleteStoryCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        DeleteStoryCommand request, CancellationToken cancellationToken)
        => await service.DeleteStoryAsync(request);
}
