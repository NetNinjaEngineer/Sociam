using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Features.Notifications.Queries.GetNotification;
using Sociam.Application.Features.Notifications.Queries.GetNotifications;
using Sociam.Application.Features.Notifications.Queries.GetReadCount;
using Sociam.Application.Features.Notifications.Queries.GetUnReadCount;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Utils;

namespace Sociam.Api.Controllers;
[Guard]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/notifications")]
public class NotificationsController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType<Result<PagedResult<NotificationDto>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotificationsAsync([FromQuery] NotificationsSpecParams @params)
        => CustomResult(await Mediator.Send(new GetNotificationsQuery { NotificationsSpecParams = @params }));

    [HttpGet("{id:guid}")]
    [ProducesResponseType<Result<NotificationDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<NotificationDto>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotificationAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetNotificationQuery { Id = id }));


    [HttpGet("me/unread-count")]
    [ProducesResponseType<Result<NotificationDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUnReadNotificationCountAsync()
        => CustomResult(await Mediator.Send(new GetUnReadCountQuery()));

    [HttpGet("me/read-count")]
    [ProducesResponseType<Result<NotificationDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReadNotificationCountAsync()
        => CustomResult(await Mediator.Send(new GetReadCountQuery()));
}
