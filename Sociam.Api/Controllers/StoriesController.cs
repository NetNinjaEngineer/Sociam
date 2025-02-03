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
using Sociam.Application.Features.Stories.Queries.GetStoryViewers;
using Sociam.Application.Features.Stories.Queries.GetUserStories;
using Sociam.Application.Features.Stories.Queries.HasUnseenStories;
using Sociam.Application.Features.Stories.Queries.IsStoryViewed;
using Sociam.Application.Features.Stories.Queries.ViewedStoriesByMe;
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

    [HttpGet("me")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<IEnumerable<StoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<StoryDto>>>> GetMyStoriesAsync()
        => CustomResult(await Mediator.Send(new GetMyStoriesQuery()));

    [HttpGet("me/stories-i-have-viewed")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<List<StoryViewedDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<List<StoryViewedDto>>>> GetStoriesViewedByMeAsync()
        => CustomResult(await Mediator.Send(new GetStoriesViewedByMeQuery()));

    [HttpGet("friends/all-active-stories")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<IEnumerable<StoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<StoryDto>>>> GetActiveStoriesAsync()
        => CustomResult(await Mediator.Send(new GetActiveFriendStoriesQuery()));

    [HttpGet("friends/{friendId:guid}/active-stories")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<UserWithStoriesDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<UserWithStoriesDto?>>> GetActiveUserStoriesAsync([FromRoute] Guid friendId)
        => CustomResult(await Mediator.Send(new GetUserStoriesQuery { FriendId = friendId.ToString() }));

    [HttpGet("friends/{friendId:guid}/has-stories-i-have-not-seen")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> GetFriendStoryStatusAsync([FromRoute] Guid friendId)
        => CustomResult(await Mediator.Send(new HasUnseenStoriesQuery { FriendId = friendId.ToString() }));

    [HttpGet("{id:guid}")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<StoryDto>>> GetStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetStoryByIdQuery { StoryId = id }));

    [HttpDelete("{id:guid}")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<bool>>> DeleteStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new DeleteStoryCommand { StoryId = id }));

    [HttpGet("{id:guid}/viewes")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryViewsResponseDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryViewsResponseDto?>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<StoryViewsResponseDto?>>> GetStoryViewsAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetStoryViewersQuery { StoryId = id }));

    [HttpGet("{id:guid}/have-i-viewed")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<bool>>> IsStoryViewedAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new IsStoryViewedQuery { StoryId = id }));

    [HttpPut("{id:guid}/mark-as-viewed")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<bool>>> ViewStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new MarkStoryAsViewedCommand { StoryId = id }));


}
