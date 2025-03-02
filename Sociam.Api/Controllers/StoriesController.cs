﻿using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.AddStoryComment;
using Sociam.Application.Features.Stories.Commands.AddStoryReaction;
using Sociam.Application.Features.Stories.Commands.ChangeStoryPrivacy;
using Sociam.Application.Features.Stories.Commands.CreateMediaStory;
using Sociam.Application.Features.Stories.Commands.CreateTextStory;
using Sociam.Application.Features.Stories.Commands.DeleteStory;
using Sociam.Application.Features.Stories.Commands.MarkAsViewed;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
using Sociam.Application.Features.Stories.Queries.GetExpiredStories;
using Sociam.Application.Features.Stories.Queries.GetMyStories;
using Sociam.Application.Features.Stories.Queries.GetStoriesByParams;
using Sociam.Application.Features.Stories.Queries.GetStoriesWithComments;
using Sociam.Application.Features.Stories.Queries.GetStoryArchive;
using Sociam.Application.Features.Stories.Queries.GetStoryById;
using Sociam.Application.Features.Stories.Queries.GetStoryComments;
using Sociam.Application.Features.Stories.Queries.GetStoryReactions;
using Sociam.Application.Features.Stories.Queries.GetStoryStatistics;
using Sociam.Application.Features.Stories.Queries.GetStoryViewers;
using Sociam.Application.Features.Stories.Queries.GetUserStories;
using Sociam.Application.Features.Stories.Queries.HasUnseenStories;
using Sociam.Application.Features.Stories.Queries.IsStoryViewed;
using Sociam.Application.Features.Stories.Queries.ViewedStoriesByMe;
using Sociam.Application.Helpers;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

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

    [HttpGet("me/stories-by-params")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<PagedResult<StoryViewsResponseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<PagedResult<StoryViewsResponseDto>>>> GetStoriesByParamsAsync([FromQuery] StoryQueryParameters @params)
        => CustomResult(await Mediator.Send(new GetStoriesByParamsQuery { StoryQueryParameters = @params }));


    [HttpGet("me/stories-i-have-viewed")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<List<StoryViewedDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<List<StoryViewedDto>>>> GetStoriesViewedByMeAsync()
        => CustomResult(await Mediator.Send(new GetStoriesViewedByMeQuery()));

    [HttpGet("me/friends/all-active-stories")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<IEnumerable<StoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<StoryDto>>>> GetActiveStoriesAsync()
        => CustomResult(await Mediator.Send(new GetActiveFriendStoriesQuery()));

    [HttpGet("me/friends/{friendId:guid}/active-stories")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<UserWithStoriesDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<UserWithStoriesDto?>>> GetActiveUserStoriesAsync([FromRoute] Guid friendId)
        => CustomResult(await Mediator.Send(new GetUserStoriesQuery { FriendId = friendId.ToString() }));

    [HttpGet("me/friends/{friendId:guid}/has-stories-i-have-not-seen")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> GetFriendStoryStatusAsync([FromRoute] Guid friendId)
        => CustomResult(await Mediator.Send(new HasUnseenStoriesQuery { FriendId = friendId.ToString() }));

    [HttpGet("{id:guid}/me")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<StoryDto>>> GetStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetStoryByIdQuery { StoryId = id }));

    [HttpDelete("{id:guid}/me")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<bool>>> DeleteStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new DeleteStoryCommand { StoryId = id }));

    [HttpGet("{id:guid}/me/viewes")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryViewsResponseDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryViewsResponseDto?>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<StoryViewsResponseDto?>>> GetStoryViewsAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetStoryViewersQuery { StoryId = id }));

    [HttpGet("{id:guid}/me/have-i-viewed")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<bool>>> IsStoryViewedAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new IsStoryViewedQuery { StoryId = id }));

    [HttpPut("{id:guid}/me/mark-as-viewed")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<bool>>> ViewStoryAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new MarkStoryAsViewedCommand { StoryId = id }));

    [HttpGet("me/expired-stories")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<PagedResult<StoryViewsResponseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<PagedResult<StoryViewsResponseDto>>>> GetExpiredStoriesAsync(
        [FromQuery] StoryQueryParameters @parameters)
        => CustomResult(await Mediator.Send(new GetExpiredStoriesQuery() { QueryParameters = @parameters }));

    [HttpGet("me/archived")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<PagedResult<StoryViewsResponseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<PagedResult<StoryViewsResponseDto>>>> GetArchivedStoriesAsync(
        [FromQuery] StoryQueryParameters @parameters)
        => CustomResult(await Mediator.Send(new GetStoryArchiveQuery() { QueryParameters = @parameters }));


    [HttpPost("me/{id:guid}/react")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<bool>>> ReactToStoryAsync(
        [FromRoute] Guid id, [FromForm] ReactionType reaction)
        => CustomResult(await Mediator.Send(new AddStoryReactionCommand { StoryId = id, ReactionType = reaction }));

    [HttpPut("me/{id:guid}/change-privacy")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<bool>>> ChangeStoryPrivacyAsync(
        [FromRoute] Guid id, [FromForm] ChangeStoryPrivacyDto request)
        => CustomResult(await Mediator.Send(
            new ChangeStoryPrivacyCommand
            {
                StoryId = id,
                Privacy = request.Privacy,
                AllowedViewerIds = request.AllowedViewerIds
            }));


    [HttpPost("me/{id:guid}/comment")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<bool>>> CommentToStoryAsync(
        [FromRoute] Guid id, [FromBody] string comment)
        => CustomResult(await Mediator.Send(new AddStoryCommentCommand() { StoryId = id, Comment = comment }));



    [HttpGet("me/with-comments")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<IEnumerable<StoryWithCommentsResponseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<StoryWithCommentsResponseDto>>>> GetStoriesWithCommentsAsync()
        => CustomResult(await Mediator.Send(new GetStoriesWithCommentsQuery()));

    [HttpGet("me/{id:guid}/with-comments")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryWithCommentsResponseDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryWithCommentsResponseDto?>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<StoryWithCommentsResponseDto?>>> GetStoryWithCommentsAsync(
        [FromRoute] Guid id) => CustomResult(await Mediator.Send(new GetStoryCommentsQuery { StoryId = id }));

    [HttpGet("me/{id:guid}/with-reactions")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryWithReactionsResponseDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryWithReactionsResponseDto?>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<StoryWithReactionsResponseDto?>>> GetStoryWithReactionsAsync(
        [FromRoute] Guid id) => CustomResult(await Mediator.Send(new GetStoryReactionsQuery { StoryId = id }));

    [HttpGet("me/{id:guid}/statistics")]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryStatisticsDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<StoryStatisticsDto?>>> GetStatisticsAsync(
        [FromRoute] Guid id) => CustomResult(await Mediator.Send(new GetStoryStatisticsQuery { StoryId = id }));
}
