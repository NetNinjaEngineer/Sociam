using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.Features.Emails.Commands.SendBulkEmails;
using Sociam.Application.Features.Emails.Commands.SendEmail;
using Sociam.Application.Features.Emails.Commands.SendEmailWithAttachments;
using Sociam.Application.Features.Emails.Commands.SendlBulkEmailsWithAttachments;

namespace Sociam.Api.Controllers;
[Route("api/emails")]
public class EmailsController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost("send")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<bool>>> SendEmailAsync(SendEmailCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("send-with-attachment")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<bool>>> SendEmailAsync([FromForm] SendEmailWithAttachmentsCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("send-bulk")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<bool>>> SendEmailAsync(SendBulkEmailsCommand command)
        => CustomResult(await Mediator.Send(command));

    [HttpPost("send-bulk-with-attachments")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<bool>>> SendEmailAsync(
        [FromForm] SendlBulkEmailsWithAttachmentsCommand command)
        => CustomResult(await Mediator.Send(command));
}
