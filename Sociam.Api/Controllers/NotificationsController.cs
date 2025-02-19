using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Notification;
using Sociam.Application.Features.Notifications.Queries.GetNotification;
using Sociam.Application.Features.Notifications.Queries.GetNotifications;

namespace Sociam.Api.Controllers;
[Guard]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/notifications")]
public class NotificationsController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<Result<NotificationDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<NotificationDto>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotificationAsync([FromRoute] Guid id)
        => CustomResult(await Mediator.Send(new GetNotificationQuery { Id = id }));

    [HttpGet]
    [ProducesResponseType<Result<IReadOnlyList<NotificationDto>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotificationsAsync()
        => CustomResult(await Mediator.Send(new GetNotificationsQuery()));
}
