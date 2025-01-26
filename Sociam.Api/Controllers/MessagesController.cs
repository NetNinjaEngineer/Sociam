using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.DTOs.Replies;
using Sociam.Application.Features.Messages.Commands.DeleteMessage;
using Sociam.Application.Features.Messages.Commands.DeleteMessageInConversation;
using Sociam.Application.Features.Messages.Commands.EditMessage;
using Sociam.Application.Features.Messages.Commands.MarkMessageAsRead;
using Sociam.Application.Features.Messages.Commands.ReplyToMessage;
using Sociam.Application.Features.Messages.Commands.SendPrivateMessage;
using Sociam.Application.Features.Messages.Commands.SendPrivateMessageByCurrentUser;
using Sociam.Application.Features.Messages.Queries.GetAll;
using Sociam.Application.Features.Messages.Queries.GetMessageById;
using Sociam.Application.Features.Messages.Queries.GetMessagesByDateRange;
using Sociam.Application.Features.Messages.Queries.GetUnreadMessages;
using Sociam.Application.Features.Messages.Queries.GetUnreadMessagesCount;
using Sociam.Application.Features.Messages.Queries.SearchMessages;
using Sociam.Application.Helpers;

namespace Sociam.Api.Controllers;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/messages")]
public class MessagesController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost("private/send")]
    [ProducesResponseType(typeof(Result<MessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<MessageDto>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<MessageDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<MessageDto>>> SendPrivateMessageAsync(
        [FromForm] SendPrivateMessageCommand command,
        [FromForm] IEnumerable<IFormFile> attachments)
    {
        command.Attachments = attachments;
        return CustomResult(await Mediator.Send(command));
    }

    [Guard(roles: [AppConstants.Roles.User])]
    [HttpPost("private/current-user/send")]
    [ProducesResponseType(typeof(Result<MessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<MessageDto>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Result<MessageDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<MessageDto>>> SendPrivateMessageByLoggedUserAsync(
        [FromForm] SendPrivateMessageByCurrentUserCommand command,
        [FromForm] IEnumerable<IFormFile> attachments)
    {
        command.Attachments = attachments;
        return CustomResult(await Mediator.Send(command));
    }

    [HttpGet("private/details")]
    [ProducesResponseType(typeof(Result<MessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<MessageDto>>> GetMessageByIdAsync([FromQuery] Guid messageId)
    {
        var getMessageQuery = new GetMessageByIdQuery { MessageId = messageId };
        return CustomResult(await Mediator.Send(getMessageQuery));
    }

    [HttpPut("private/mark-as-read")]
    public async Task<ActionResult<Result<bool>>> MarkMessageAsReadAsync(
        [FromQuery] MarkMessageAsReadCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPut("private/edit")]
    public async Task<ActionResult<Result<bool>>> EditMessageAsync(
        [FromQuery] Guid messageId,
        [FromQuery] string NewContent)
        => CustomResult(await Mediator.Send(new EditMessageCommand { MessageId = messageId, NewContent = NewContent }));

    [HttpDelete("private/delete")]
    public async Task<ActionResult<Result<bool>>> DeleteMessageAsync(
        [FromQuery] Guid messageId)
        => CustomResult(await Mediator.Send(new DeleteMessageCommand { MessageId = messageId }));

    [HttpGet("private/unread-count")]
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<int>>> GetUnreadMessagesCountAsync([FromQuery] GetUnreadMessagesCountQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpGet("private/by-date-range")]
    [ProducesResponseType(typeof(Result<IEnumerable<MessageDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<MessageDto>>>> GetMessagesByDateRangeAsync([FromQuery] GetMessagesByDateRangeQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpGet("private/unread-messages")]
    [ProducesResponseType(typeof(Result<IEnumerable<MessageDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<MessageDto>>>> GetUnreadMessagesAsync([FromQuery] GetUnreadMessagesQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpGet("private/search")]
    [ProducesResponseType(typeof(Result<IEnumerable<MessageDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<MessageDto>>>> SearchMessagesAsync([FromQuery] SearchMessagesQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpDelete("private/{conversationId:guid}/{messageId:guid}")]
    public async Task<ActionResult<Result<bool>>> DeleteMessageInConversationAsync(
        [FromRoute] Guid conversationId, [FromRoute] Guid messageId)
      => CustomResult(await Mediator.Send(new DeleteMessageInConversationCommand { MessageId = messageId, ConversationId = conversationId }));

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllMessagesAsync([FromQuery] GetAllQuery query)
        => CustomResult(await Mediator.Send(query));

    [HttpPost("private/{messageId}/reply")]
    [ProducesResponseType(typeof(Result<MessageReplyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<MessageReplyDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<MessageReplyDto>>> ReplyToPrivateMessageAsync(
        [FromRoute] Guid messageId,
        [FromQuery] string content)
    {
        var command = new ReplyToMessageCommand { MessageId = messageId, Content = content };
        return CustomResult(await Mediator.Send(command));
    }
}