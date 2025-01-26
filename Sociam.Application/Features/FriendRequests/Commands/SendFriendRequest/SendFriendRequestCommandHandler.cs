using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Commands.SendFriendRequest;
public sealed class SendFriendRequestCommandHandler(
    IFriendshipService friendshipService) : IRequestHandler<SendFriendRequestCommand, Result<FriendshipResponseDto>>
{
    public async Task<Result<FriendshipResponseDto>> Handle(SendFriendRequestCommand request,
        CancellationToken cancellationToken)
        => await friendshipService.SendFriendRequestAsync(request.SendFriendshipRequest);
}
