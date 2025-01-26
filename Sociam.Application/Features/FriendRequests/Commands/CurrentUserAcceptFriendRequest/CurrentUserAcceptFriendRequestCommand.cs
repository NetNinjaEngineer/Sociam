using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;

namespace Sociam.Application.Features.FriendRequests.Commands.CurrentUserAcceptFriendRequest;
public sealed class CurrentUserAcceptFriendRequestCommand : IRequest<Result<FriendshipResponseDto>>
{
    public required Guid FriendshipId { get; set; }
}
