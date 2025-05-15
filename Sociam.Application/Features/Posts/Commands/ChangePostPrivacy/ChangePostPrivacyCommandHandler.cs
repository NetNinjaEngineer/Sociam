using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Posts.Commands.ChangePostPrivacy;

public sealed class ChangePostPrivacyCommandHandler(IPostsService service) : IRequestHandler<ChangePostPrivacyCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(ChangePostPrivacyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
