using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.Features.Groups.Commands.AddUserToGroup;
using Sociam.Application.Features.Groups.Commands.CreateNewGroup;
using Sociam.Application.Features.Groups.Queries.GetAllGroups;
using Sociam.Application.Features.Groups.Queries.GetGroupById;
using Sociam.Application.Helpers;

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
    [HttpGet("{groupId}")]
    [ProducesResponseType(typeof(Result<GroupListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<GroupListDto>>> GetGroupByIdAsync([FromRoute] Guid groupId)
        => CustomResult(await Mediator.Send(new GetGroupByIdQuery { GroupId = groupId }));

}
