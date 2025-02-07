using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.ChangeStoryPrivacy;
public sealed class ChangeStoryPrivacyCommand : IRequest<Result<bool>>
{
    public Guid StoryId { get; set; }
    public StoryPrivacy Privacy { get; set; }
    public List<string>? AllowedViewerIds { get; set; }
}