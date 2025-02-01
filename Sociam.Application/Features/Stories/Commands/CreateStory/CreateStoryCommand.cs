using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.CreateStory;
public sealed class CreateStoryCommand : IRequest<Result<StoryDto>>
{
    public IFormFile Media { get; set; } = null!;

    public MediaType MediaType { get; set; }

    public string? Caption { get; set; }
    public StoryPrivacy StoryPrivacy { get; set; }

    public List<string> HashTags { get; set; } = [];
}
