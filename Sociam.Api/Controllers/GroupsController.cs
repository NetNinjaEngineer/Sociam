using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.Features.Groups.Commands.AddUserToGroup;
using Sociam.Application.Features.Groups.Commands.CreateNewGroup;
using Sociam.Application.Helpers;

namespace Sociam.Api.Controllers;
[Guard(roles: [AppConstants.Roles.User])]
[Route("api/groups")]
public class GroupsController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost("new")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<Guid>>> CreateNewGroupAsync([FromForm] CreateNewGroupCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("{groupId}/addUser")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> AddUserToGroupAsync([FromRoute] Guid groupId, [FromQuery] Guid userId)
    {
        var command = new AddUserToGroupCommand { GroupId = groupId, UserId = userId };
        return CustomResult(await Mediator.Send(command));
    }

}
