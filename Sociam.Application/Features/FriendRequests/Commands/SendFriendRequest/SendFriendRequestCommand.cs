using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;

namespace Sociam.Application.Features.FriendRequests.Commands.SendFriendRequest;
public sealed class SendFriendRequestCommand : IRequest<Result<FriendshipResponseDto>>
{
    public SendFriendshipRequestDto SendFriendshipRequest { get; set; } = null!;
}
