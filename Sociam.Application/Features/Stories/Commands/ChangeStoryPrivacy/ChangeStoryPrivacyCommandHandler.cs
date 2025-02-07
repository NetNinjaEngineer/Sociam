using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Stories.Commands.ChangeStoryPrivacy;
public sealed class ChangeStoryPrivacyCommandHandler(IStoryService service)
    : IRequestHandler<ChangeStoryPrivacyCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        ChangeStoryPrivacyCommand request, CancellationToken cancellationToken)
        => await service.ChangeStoryPrivacy(request);
}
