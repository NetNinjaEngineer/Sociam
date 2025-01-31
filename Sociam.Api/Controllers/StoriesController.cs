using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
using Sociam.Application.Helpers;

namespace Sociam.Api.Controllers;
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/stories")]
[ApiController]
public class StoriesController(IMediator mediator) : ApiBaseController(mediator)
{
    [Route("create")]
    [HttpPost]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<StoryDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<StoryDto>>> CreateStoryAsync(CreateStoryCommand command)
        => CustomResult(await Mediator.Send(command));

    [Route("friends/active-stories")]
    [HttpGet]
    [Guard(roles: [AppConstants.Roles.User])]
    [ProducesResponseType(typeof(Result<IEnumerable<StoryDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IEnumerable<StoryDto>>>> GetActiveStoriesAsync()
        => CustomResult(await Mediator.Send(new GetActiveFriendStoriesQuery()));
}
