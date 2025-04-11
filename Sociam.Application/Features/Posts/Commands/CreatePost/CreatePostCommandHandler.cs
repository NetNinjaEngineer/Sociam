using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Posts.Commands.CreatePost
{
    public sealed class CreatePostCommandHandler(IPostsService service) : IRequestHandler<CreatePostCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(
            CreatePostCommand request, CancellationToken cancellationToken)
        {
            return await service.CreatePostAsync(request);
        }
    }
}
