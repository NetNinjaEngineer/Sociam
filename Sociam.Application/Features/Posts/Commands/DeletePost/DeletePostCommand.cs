using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Posts.Commands.DeletePost;

public sealed class DeletePostCommand(Guid postId) : IRequest<Result<Unit>>
{
    public Guid PostId { get; } = postId;
}