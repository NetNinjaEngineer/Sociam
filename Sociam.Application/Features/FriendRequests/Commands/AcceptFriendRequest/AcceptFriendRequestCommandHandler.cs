using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.FriendRequests.Commands.AcceptFriendRequest;
public sealed class AcceptFriendRequestCommandHandler(IFriendshipService friendshipService) :
    IRequestHandler<AcceptFriendRequestCommand, Result<FriendshipResponseDto>>
{
    public async Task<Result<FriendshipResponseDto>> Handle(
        AcceptFriendRequestCommand request,
        CancellationToken cancellationToken)
        => await friendshipService.AcceptFriendRequestAsync(request.AcceptFriendRequest);
}
