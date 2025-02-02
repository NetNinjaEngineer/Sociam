using Sociam.Domain.Enums;

namespace Sociam.Domain.Interfaces.DataTransferObjects;

public sealed class StoryForReturnDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public StoryPrivacy StoryPrivacy { get; set; }
    public int ViewersCount { get; set; }
    public int ReactionsCount { get; set; }
    public int CommentsCount { get; set; }
}