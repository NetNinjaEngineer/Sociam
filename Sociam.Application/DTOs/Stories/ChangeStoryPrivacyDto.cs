using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Stories;

public sealed class ChangeStoryPrivacyDto
{
    public StoryPrivacy Privacy { get; set; }
    public List<string>? AllowedViewerIds { get; set; }
}