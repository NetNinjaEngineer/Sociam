using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Features.Users.Queries.GetUserProfile;
using Sociam.Application.Helpers;

namespace Sociam.Api.Controllers;
[Guard(roles: [AppConstants.Roles.User])]
[ApiVersion(1.0)]
[Route("api/v{apiVersion:apiVersion}/users")]
public class UsersController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpGet("me")]
    [ProducesResponseType(typeof(Result<ProfileDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ProfileDto?>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserProfileAsync()
        => CustomResult(await Mediator.Send(new GetUserProfileQuery()));
}
