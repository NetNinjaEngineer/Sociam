using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.Features.Followings.Commands.FollowUser;
using Sociam.Application.Features.Followings.Commands.UnFollowUser;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Guard]
[Route("api/v{version:apiVersion}/followings")]
[ApiController]
public class FollowingsController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost("follow")]
    public async Task<ActionResult<Result<bool>>> FollowUserAsync(
        [FromQuery] string followerId,
        [FromQuery] string followedId)
        => CustomResult(
            await Mediator.Send(new FollowUserCommand { FollowerId = followerId, FollowedId = followedId }));

    [HttpPost("unfollow")]
    public async Task<ActionResult<Result<bool>>> UnFollowUserAsync(
        [FromQuery] string followerId,
        [FromQuery] string followedId)
        => CustomResult(
            await Mediator.Send(new UnFollowUserCommand { FollowerId = followerId, FollowedId = followedId }));
}
