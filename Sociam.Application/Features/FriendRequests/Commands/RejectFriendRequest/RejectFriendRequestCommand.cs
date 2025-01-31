using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.FriendRequests.Commands.RejectFriendRequest;
public sealed class RejectFriendRequestCommand : IRequest<Result<bool>>
{
    public Guid FriendRequestId { get; set; }
}
