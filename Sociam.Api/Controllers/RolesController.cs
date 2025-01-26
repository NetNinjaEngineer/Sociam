using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.Features.Roles.Commands.AddClaimToRole;
using Sociam.Application.Features.Roles.Commands.AssignClaimToUser;
using Sociam.Application.Features.Roles.Commands.AssignRoleToUser;
using Sociam.Application.Features.Roles.Commands.CreateRole;
using Sociam.Application.Features.Roles.Commands.DeleteRole;
using Sociam.Application.Features.Roles.Commands.EditRole;
using Sociam.Application.Features.Roles.Queries.GetAllRoles;
using Sociam.Application.Features.Roles.Queries.GetRoleClaims;
using Sociam.Application.Features.Roles.Queries.GetUserClaims;
using Sociam.Application.Features.Roles.Queries.GetUserRoles;

namespace Sociam.Api.Controllers;
[Route("api/roles")]
[ApiController]
public class RolesController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpGet("getAllRoles")]
    [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetAllRolesAsync()
        => CustomResult(await Mediator.Send(new GetAllRolesQuery()));

    [HttpGet("getRoleClaims")]
    [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetRoleClaimsAsync(
        [FromQuery] GetRoleClaimsQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpGet("getUserClaims")]
    [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetUserClaimsAsync(
        [FromQuery] GetUserClaimsQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpGet("getUserRoles")]
    [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<string>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<IEnumerable<string>>>> GetUserRolesAsync([FromQuery] GetUserRolesQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpPost("addClaimToRole")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AddClaimToRoleAsync(AddClaimToRoleCommand command)
       => CustomResult(await Mediator.Send(command));

    [HttpPost("assignClaimToUser")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AssignClaimToUserAsync(AssignClaimToUserCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("assignRoleToUser")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> AssignRoleToUserAsync(AssignRoleToUserCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("createRole")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> CreateRoleAsync(CreateRoleCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("deleteRole")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> DeleteRoleAsync(DeleteRoleCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("editRole")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> EditRoleAsync(EditRoleCommand command)
        => CustomResult(await Mediator.Send(command));
}
