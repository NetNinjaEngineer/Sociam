using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Posts.Commands.EditPost
{
    public sealed class EditPostCommandHandler(IPostsService service) : IRequestHandler<EditPostCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            return await service.EditPostAsync(request);
        }
    }
}
