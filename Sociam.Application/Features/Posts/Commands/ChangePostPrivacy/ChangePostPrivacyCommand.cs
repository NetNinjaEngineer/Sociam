using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Posts.Commands.ChangePostPrivacy;

public sealed class ChangePostPrivacyCommand(Guid postId, PostPrivacy privacy) : IRequest<Result<bool>>
{
    public Guid PostId { get; } = postId;
    public PostPrivacy Privacy { get; } = privacy;
}
