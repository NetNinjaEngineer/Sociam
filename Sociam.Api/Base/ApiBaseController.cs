﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Application.Bases;
using System.Net;

namespace Sociam.Api.Base;
[ApiKey]
[ApiController]
public class ApiBaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator;

    protected ActionResult CustomResult<T>(Result<T> result)
    {
        return GetObjectResult(result);
    }

    private static ActionResult GetObjectResult<T>(Result<T> result) =>
        result.StatusCode switch
        {
            HttpStatusCode.OK => new OkObjectResult(result),
            HttpStatusCode.BadRequest => new BadRequestObjectResult(result),
            HttpStatusCode.NotFound => new NotFoundObjectResult(result),
            HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(result),
            HttpStatusCode.Conflict => new ConflictObjectResult(result),
            HttpStatusCode.NoContent => new NoContentResult(),
            HttpStatusCode.Created => new ObjectResult(result),
            HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(result),
            HttpStatusCode.Forbidden => new ForbidResult(),
            _ => throw new InvalidOperationException()
        };
}