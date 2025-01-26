using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Commands.CurrentUserAcceptFriendRequest;
public sealed class CurrentUserAcceptFriendRequestCommandHandler(IFriendshipService friendshipService) :
    IRequestHandler<CurrentUserAcceptFriendRequestCommand, Result<FriendshipResponseDto>>
{
    public async Task<Result<FriendshipResponseDto>> Handle(
        CurrentUserAcceptFriendRequestCommand request, CancellationToken cancellationToken)
        => await friendshipService.CurrentUserAcceptFriendRequestAsync(request);
}
