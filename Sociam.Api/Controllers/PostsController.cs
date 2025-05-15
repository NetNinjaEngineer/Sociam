using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Post;
using Sociam.Application.Features.Posts.Commands.AddReaction;
using Sociam.Application.Features.Posts.Commands.ChangePostPrivacy;
using Sociam.Application.Features.Posts.Commands.CreatePost;
using Sociam.Application.Features.Posts.Commands.DeletePost;
using Sociam.Application.Features.Posts.Commands.EditPost;
using Sociam.Application.Features.Posts.Commands.RemoveReaction;
using Sociam.Application.Features.Posts.Queries.GetPosts;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Api.Controllers;

[Guard]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/posts")]
public class PostsController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePostAsync([FromForm] CreatePostCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> EditPostAsync([FromForm] EditPostCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpDelete("{postId:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePostAsync([FromRoute] Guid postId)
        => CustomResult(await Mediator.Send(new DeletePostCommand(postId)));

    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedResult<PostDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserPostsAsync([FromQuery] PostsParams postsParams)
        => CustomResult(await Mediator.Send(new GetPostsQuery { PostsParams = postsParams }));

    [HttpPost("{postId:guid}/reactions")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddReactionAsync([FromRoute] Guid postId, [FromForm] AddPostReactionDto request)
        => CustomResult(await Mediator.Send(new AddReactionCommand { PostId = postId, Reaction = request.ReactionType }));

    [HttpDelete("{postId:guid}/reactions/{reactionId:guid}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RemoveReactionAsync([FromRoute] Guid postId, [FromRoute] Guid reactionId)
        => CustomResult(await Mediator.Send(new RemoveReactionCommand() { PostId = postId, ReactionId = reactionId }));

    [HttpPut("{postId:guid}/change-privacy")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ChangePostPrivacyAsync([FromRoute] Guid postId, [FromForm] ChangePostPrivacyDto request)
        => CustomResult(await Mediator.Send(new ChangePostPrivacyCommand(postId, request.PostPrivacy)));

}