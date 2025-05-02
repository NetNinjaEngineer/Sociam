using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Features.Users.Commands.ChangeAccountEmail;
using Sociam.Application.Features.Users.Commands.ChangeAccountPassword;
using Sociam.Application.Features.Users.Commands.UpdateAvatar;
using Sociam.Application.Features.Users.Commands.UpdateCover;
using Sociam.Application.Features.Users.Commands.UpdateUserProfile;
using Sociam.Application.Features.Users.Commands.VerifyChangeEmail;
using Sociam.Application.Features.Users.Queries.GetTrustedDevices;
using Sociam.Application.Features.Users.Queries.GetUsernameSuggestions;
using Sociam.Application.Features.Users.Queries.GetUserProfile;
using Sociam.Application.Features.Users.Queries.IsEmailTaken;
using Sociam.Application.Features.Users.Queries.IsUsernameAvailable;
using Sociam.Application.Helpers;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{apiVersion:apiVersion}/users")]
public class UsersController(IMediator mediator) : ApiBaseController(mediator)
{
    [Guard(roles: [AppConstants.Roles.User])]
    [HttpGet("me")]
    [ProducesResponseType(typeof(Result<ProfileDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ProfileDto?>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserProfileAsync()
        => CustomResult(await Mediator.Send(new GetUserProfileQuery()));

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPut("me")]
    [ProducesResponseType(typeof(Result<ProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ProfileDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UpdateUserProfileCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPatch("me/avatar")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateUserAvatarAsync([FromForm] UpdateAvatarCommand command)
        => CustomResult(await Mediator.Send(command));


    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPatch("me/cover")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateUserCoverAsync([FromForm] UpdateCoverCommand command)
        => CustomResult(await Mediator.Send(command));

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPatch("me/change-account-email")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> ChangeAccountEmailAsync([FromBody] ChangeAccountEmailCommand command)
        => CustomResult(await Mediator.Send(command));


    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPatch("me/verify-change-account-email")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> VerifyChangeAccountEmailAsync([FromBody] VerifyChangeEmailCommand command)
        => CustomResult(await Mediator.Send(command));


    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPatch("me/change-password")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> ChangeAccountPasswordAsync([FromBody] ChangeAccountPasswordCommand command)
        => CustomResult(await Mediator.Send(command));


    [Guard(roles: [AppConstants.Roles.User])]
    [HttpGet("me/trusted-devices")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserTrustedDevicesAsync()
        => CustomResult(await Mediator.Send(new GetTrustedDevicesQuery()));

    [HttpGet("username-available/{username}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> IsUsernameAvailableAsync([FromRoute] string username)
        => CustomResult(await Mediator.Send(new IsUsernameAvailableQuery { Username = username }));

    [HttpGet("username-suggestions/{username}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsernameSuggestionsAsync([FromRoute] string username)
        => CustomResult(await Mediator.Send(new GetUsernameSuggestionsQuery() { Username = username }));

    // api/v1/users/email-taken?email=me5260287@gmail.com
    [HttpGet("email-taken")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> IsEmailTakenAsync([FromQuery] string email)
       => CustomResult(await Mediator.Send(new IsEmailTakenQuery(email)));
}
