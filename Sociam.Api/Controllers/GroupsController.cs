using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Features.Groups.Commands.AddUserToGroup;
using Sociam.Application.Features.Groups.Commands.CreateNewGroup;
using Sociam.Application.Features.Groups.Commands.HandleJoinRequest;
using Sociam.Application.Features.Groups.Commands.JoinGroup;
using Sociam.Application.Features.Groups.Commands.RemoveMember;
using Sociam.Application.Features.Groups.Commands.SendGroupMessage;
using Sociam.Application.Features.Groups.Queries.GetAllGroups;
using Sociam.Application.Features.Groups.Queries.GetGroup;
using Sociam.Application.Features.Groups.Queries.GetGroupById;
using Sociam.Application.Features.Groups.Queries.GetGroupsWithParams;
using Sociam.Application.Helpers;
using Sociam.Domain.Enums;
using Sociam.Domain.Utils;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/groups")]
public class GroupsController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost("")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
    public async Task<ActionResult<Result<Guid>>> CreateGroupAsync([FromForm] CreateNewGroupCommand command)
    {
        var result = await Mediator.Send(command);
        return CustomResult(result);
    }

    [HttpPost("{groupId}/users/{userId}")]
    [Guard(roles: [AppConstants.Roles.User, AppConstants.Roles.Admin])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<bool>>> AddUserToGroupAsync([FromRoute] Guid groupId, [FromRoute] Guid userId)
    {
        var command = new AddUserToGroupCommand { GroupId = groupId, UserId = userId };
        return CustomResult(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpGet("")]
    [ProducesResponseType(typeof(Result<IReadOnlyList<GroupListDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IReadOnlyList<GroupListDto>>>> GetAllGroupsAsync()
        => CustomResult(await Mediator.Send(new GetAllGroupsQuery()));

    [AllowAnonymous]
    [HttpGet("by")]
    public async Task<IActionResult> GetGroupsByParamsAsync([FromQuery] GroupParams @params)
    {
        var result = await Mediator.Send(new GetGroupsWithParamsQuery() { GroupParams = @params });
        if (result.IsLeft)
            return Ok(result.Left);

        return Ok(result.Right);
    }

    [AllowAnonymous]
    [HttpGet("{groupId}")]
    [ProducesResponseType(typeof(Result<GroupListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<GroupListDto>>> GetGroupByIdAsync([FromRoute] Guid groupId)
        => CustomResult(await Mediator.Send(new GetGroupByIdQuery { GroupId = groupId }));

    [Guard]
    [HttpGet("view")]
    [ProducesResponseType(typeof(Result<GroupListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<GroupListDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<GroupListDto>>> GetGroupAsync([FromQuery] Guid id)
        => CustomResult(await Mediator.Send(new GetGroupQuery { GroupId = id }));

    [HttpPost("{id}/members/join")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<string>>> JoinGroupAsync([FromRoute] Guid id)
       => CustomResult(await Mediator.Send(new JoinGroupCommand { GroupId = id }));

    [Guard(roles: [AppConstants.Roles.Admin])]
    [HttpPut("{groupId}/requests/{requestId}")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<string>>> HandleJoinGroupAsync(
        [FromRoute] Guid groupId,
        [FromRoute] Guid requestId,
        [FromForm] JoinRequestStatus joinRequestStatus)
        => CustomResult(await Mediator.Send(new HandleJoinRequestCommand { GroupId = groupId, RequestId = requestId, JoinStatus = joinRequestStatus }));


    // DELETE api/v1/groups/{groupId}/members/{memberId}
    [Guard]
    [HttpDelete("{groupId}/members/{memberId}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<bool>>> RemoveMemberAsync(
       [FromRoute] Guid groupId,
       [FromRoute] Guid memberId)
       => CustomResult(await Mediator.Send(new RemoveMemberCommand { GroupId = groupId, MemberId = memberId }));

    [Guard]
    [HttpPost("{groupId:guid}/conversations/{conversationId:guid}/messages")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> SendMessage(
        [FromRoute] Guid groupId,
        [FromRoute] Guid conversationId,
        [FromForm] SendGroupMessageDto dto)
    {
        SendGroupMessageCommand command = new()
        {
            GroupId = groupId,
            GroupConversationId = conversationId,
            Content = dto.Content,
            Attachments = dto.Attachments
        };

        return CustomResult(await Mediator.Send(command));
    }
}
