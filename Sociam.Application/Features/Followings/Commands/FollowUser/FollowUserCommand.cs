using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Followings.Commands.FollowUser;
public sealed class FollowUserCommand : IRequest<Result<bool>>
{
    public required string FollowerId { get; set; }

    public required string FollowedId { get; set; }
}
