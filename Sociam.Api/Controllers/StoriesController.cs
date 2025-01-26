using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;
using Sociam.Application.Helpers;

namespace Sociam.Api.Controllers;
[Route("api/stories")]
[ApiController]
public class StoriesController(IMediator mediator) : ApiBaseController(mediator)
{
    [HttpPost("create")]
    [Guard(roles: [AppConstants.Roles.User])]
    public async Task<ActionResult<Result<StoryDto>>> CreateStoryAsync(CreateStoryCommand command)
        => CustomResult(await Mediator.Send(command));
}
