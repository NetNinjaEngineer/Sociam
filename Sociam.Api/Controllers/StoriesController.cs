using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;
using Sociam.Application.Features.Stories.Commands.DeleteStory;
using Sociam.Application.Features.Stories.Commands.MarkAsViewed;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
using Sociam.Application.Features.Stories.Queries.GetMyStories;
using Sociam.Application.Helpers;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/stories")]
[ApiController]
public class StoriesController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<StoryDto>>> CreateStoryAsync(CreateStoryCommand command)
        => CustomResult(await Mediator.Send(command));

    [Route("friends/active-stories")]
    [HttpGet]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<IEnumerable<StoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<StoryDto>>>> GetActiveStoriesAsync()
        => CustomResult(await Mediator.Send(new GetActiveFriendStoriesQuery()));


    [HttpPut("{storyId:guid}/me/view")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<bool>>> ViewStoryAsync([FromRoute] Guid storyId)
        => CustomResult(await Mediator.Send(new MarkStoryAsViewedCommand() { StoryId = storyId }));

    [HttpDelete("{storyId:guid}/me")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<bool>>> DeleteStoryAsync([FromRoute] Guid storyId)
        => CustomResult(await Mediator.Send(new DeleteStoryCommand() { StoryId = storyId }));

    [HttpGet("me")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> GetMyStoriesAsync()
        => CustomResult(await Mediator.Send(new GetMyStoriesQuery()));
}