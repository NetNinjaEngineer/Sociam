using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;

namespace Sociam.Application.Features.FriendRequests.Commands.AcceptFriendRequest;
public sealed class AcceptFriendRequestCommand : IRequest<Result<FriendshipResponseDto>>
{
    public AcceptFriendRequestDto AcceptFriendRequest { get; set; } = null!;
}
