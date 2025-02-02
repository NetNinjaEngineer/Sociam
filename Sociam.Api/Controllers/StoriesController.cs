using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateMediaStory;
using Sociam.Application.Features.Stories.Commands.CreateTextStory;
using Sociam.Application.Features.Stories.Commands.DeleteStory;
using Sociam.Application.Features.Stories.Commands.MarkAsViewed;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
using Sociam.Application.Features.Stories.Queries.GetMyStories;
using Sociam.Application.Features.Stories.Queries.GetStoryById;
using Sociam.Application.Helpers;
using Sociam.Domain.Interfaces.DataTransferObjects;

namespace Sociam.Api.Controllers;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/stories")]
[ApiController]
public class StoriesController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost("media")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<MediaStoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<MediaStoryDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<MediaStoryDto>>> CreateMediaStoryAsync(
        [FromForm] CreateMediaStoryCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("text")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<TextStoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<TextStoryDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<TextStoryDto>>> CreateTextStoryAsync(
        [FromForm] CreateTextStoryCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpGet("friends/active-stories")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<IEnumerable<StoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<StoryDto>>>> GetActiveStoriesAsync()
        => CustomResult(await Mediator.Send(new GetActiveFriendStoriesQuery()));

    [HttpPut("{id:guid}/me/view")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<bool>>> ViewStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new MarkStoryAsViewedCommand { StoryId = id }));

    [HttpDelete("{id:guid}/me")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<bool>>> DeleteStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new DeleteStoryCommand { StoryId = id }));

    [HttpGet("me")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<IEnumerable<StoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<StoryDto>>>> GetMyStoriesAsync()
        => CustomResult(await Mediator.Send(new GetMyStoriesQuery()));

    [HttpGet("{id:guid}/me")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<StoryDto>>> GetStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetStoryByIdQuery { StoryId = id }));
}
