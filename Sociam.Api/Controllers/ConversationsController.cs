using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.Features.Conversations.Commands.CreateGroupConversation;
using Sociam.Application.Features.Conversations.Commands.DeleteConversation;
using Sociam.Application.Features.Conversations.Commands.StartConversation;
using Sociam.Application.Features.Conversations.Queries.GetConversation;
using Sociam.Application.Features.Conversations.Queries.GetConversationMessages;
using Sociam.Application.Features.Conversations.Queries.GetPagedConversationMessages;
using Sociam.Application.Features.Conversations.Queries.GetUserConversation;
using Sociam.Application.Helpers;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Guard(roles: [AppConstants.Roles.User])]
[Route("api/v{version:apiVersion}/conversations")]
public class ConversationsController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost("private/start")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Result<string>>> StartConversationAsync(StartConversationCommand command)
        => CustomResult(await Mediator.Send(command));


    [HttpGet("messages")]
    [ProducesResponseType(typeof(Result<ConversationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<ConversationDto>>> GetConversationMessagesAsync([FromQuery] Guid conversationId)
    {
        var conversationQuery = new GetConversationMessagesQuery() { ConversationId = conversationId };
        return CustomResult(await Mediator.Send(conversationQuery));
    }

    [HttpGet("messages/paged")]
    [ProducesResponseType(typeof(Result<ConversationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<ConversationDto>>> GetPagedConversationMessagesAsync(
        [FromQuery] GetPagedConversationMessagesQuery query) => CustomResult(await Mediator.Send(query));


    [HttpGet("private/user")]
    [ProducesResponseType(typeof(Result<ConversationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<ConversationDto>>> GetUserConversationAsync(
        [FromQuery] GetUserConversationQuery query) => CustomResult(await Mediator.Send(query));

    [HttpGet("private/between")]
    [ProducesResponseType(typeof(Result<ConversationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ConversationDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<ConversationDto>>> GetConversationBetweenAsync(
        [FromQuery] GetConversationQuery query) => CustomResult(await Mediator.Send(query));

    [HttpDelete("private")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<bool>>> DeleteConversationAsync(
        [FromQuery] DeleteConversationCommand command) => CustomResult(await Mediator.Send(command));

    [HttpPost("group/start")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Guid>>> CreateGroupConversationAsync([FromBody] CreateGroupConversationCommand command)
        => CustomResult(await Mediator.Send(command));
}
