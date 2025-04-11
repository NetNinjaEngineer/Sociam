using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sociam.Api.Attributes;
using Sociam.Api.Base;
using Sociam.Application.Bases;
using Sociam.Application.Features.Posts.Commands.CreatePost;

namespace Sociam.Api.Controllers
{
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
    }
}
