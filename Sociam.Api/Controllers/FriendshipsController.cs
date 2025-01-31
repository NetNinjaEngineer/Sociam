using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Features.FriendRequests.Commands.AcceptFriendRequest;
using Sociam.Application.Features.FriendRequests.Commands.CurrentUserAcceptFriendRequest;
using Sociam.Application.Features.FriendRequests.Commands.CurrentUserSendFriendRequest;
using Sociam.Application.Features.FriendRequests.Commands.RejectFriendRequest;
using Sociam.Application.Features.FriendRequests.Commands.SendFriendRequest;
using Sociam.Application.Features.FriendRequests.Queries.AreFriendsForCurrentUser;
using Sociam.Application.Features.FriendRequests.Queries.CheckIfAreFriends;
using Sociam.Application.Features.FriendRequests.Queries.GetCurrentUserReceivedFriendRequests;
using Sociam.Application.Features.FriendRequests.Queries.GetFriends;
using Sociam.Application.Features.FriendRequests.Queries.GetLoggedInUserRequestedFriendships;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/friendships")]
public class FriendshipsController(IMediator mediator) : ApiBaseController(mediator)
{
    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPost("request")]
    public async Task<ActionResult<Result<FriendshipResponseDto>>> SendFriendshipRequestAsync(SendFriendshipRequestDto request)
        => CustomResult(await Mediator.Send(new SendFriendRequestCommand() { SendFriendshipRequest = request }));

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPost("{friendRequestId}/accept")]
    public async Task<ActionResult<Result<FriendshipResponseDto>>> AcceptFriendRequestAsync(
        [FromRoute] Guid friendRequestId,
        [FromQuery] string userId)
    {
        var request = new AcceptFriendRequestDto(userId, friendRequestId);
        return CustomResult(await Mediator.Send(new AcceptFriendRequestCommand { AcceptFriendRequest = request }));
    }

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpGet("me/sent")]
    [ProducesResponseType(typeof(Result<IEnumerable<PendingFriendshipRequest>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<PendingFriendshipRequest>>>> GetLoggedInUserSentFriendshipsAsync()
        => CustomResult(await Mediator.Send(new GetLoggedInUserRequestedFriendshipsQuery()));


    [Guard(roles: [AppConstants.Roles.User])]
    [HttpGet("me/received")]
    [ProducesResponseType(typeof(Result<IEnumerable<PendingFriendshipRequest>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<PendingFriendshipRequest>>>> GetLoggedInUserReceivedFriendshipsAsync()
        => CustomResult(await Mediator.Send(new GetCurrentUserReceivedFriendRequestsQuery()));

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpGet("me/friends")]
    public async Task<ActionResult<Result<List<ApplicationUser>>>> GetMyFriendsAsync()
        => CustomResult(await Mediator.Send(new GetFriendsQuery()));

    [HttpGet("are-friends")]
    public async Task<ActionResult<Result<bool>>> AreFriendsAsync(
        [FromQuery] CheckIfAreFriendsQuery request)
    {
        return CustomResult(await Mediator.Send(request));
    }

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpGet("me/are-friends")]
    public async Task<ActionResult<Result<bool>>> AreFriendsWithCurrentUserAsync(
        [FromQuery] AreFriendsForCurrentUserQuery request)
        => CustomResult(await Mediator.Send(request));

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPost("me/request")]
    public async Task<ActionResult<Result<FriendshipResponseDto>>> SendFriendshipCurrentUserAsync(
        [FromQuery] CurrentUserSendFriendRequestCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPut("{friendRequestId}/me/accept")]
    public async Task<ActionResult<Result<FriendshipResponseDto>>> AcceptFriendRequestCurrentUserAsync([FromRoute] Guid friendRequestId) =>
        CustomResult(await Mediator.Send(new CurrentUserAcceptFriendRequestCommand { FriendshipId = friendRequestId }));

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPut("{friendRequestId}/me/reject")]
    public async Task<ActionResult<Result<bool>>> MeRejectFriendRequestAsync([FromRoute] Guid friendRequestId)
        => CustomResult(await Mediator.Send(new RejectFriendRequestCommand() { FriendRequestId = friendRequestId }));


}