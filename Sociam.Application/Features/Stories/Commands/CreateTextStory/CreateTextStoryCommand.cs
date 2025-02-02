using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.CreateTextStory;
public sealed class CreateTextStoryCommand : IRequest<Result<TextStoryDto>>
{
    public string Content { get; set; } = null!;
    public List<string>? HashTags { get; set; } = null!;
    public StoryPrivacy StoryPrivacy { get; set; }
    public List<string>? AllowedViewerIds { get; set; }
}