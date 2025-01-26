using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Commands.CurrentUserSendFriendRequest;
public sealed class CurrentUserSendFriendRequestCommandHandler(
    IFriendshipService friendshipService) : IRequestHandler<CurrentUserSendFriendRequestCommand, Result<FriendshipResponseDto>>
{
    public async Task<Result<FriendshipResponseDto>> Handle(
        CurrentUserSendFriendRequestCommand request, CancellationToken cancellationToken)
        => await friendshipService.SendFriendRequestCurrentUserAsync(request);
}
