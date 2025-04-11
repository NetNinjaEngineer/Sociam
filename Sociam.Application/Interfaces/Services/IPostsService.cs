using Sociam.Application.Bases;
using Sociam.Application.Features.Posts.Commands.CreatePost;

namespace Sociam.Application.Interfaces.Services
{
    public interface IPostsService
    {
        Task<Result<Guid>> CreatePostAsync(CreatePostCommand command);
    }
}
