using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Post;

public sealed class PostReactionDto
{
    public Guid PostId { get; init; }
    public Guid ReactionId { get; init; }
    public DateTimeOffset ReactedAt { get; init; }
    public ReactionType Type { get; init; }
    public string ReactedById { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string? ProfilePictureUrl { get; init; }
    public string DisplayName { get; init; } = null!;
    public string RelativeTime { get; init; } = null!;
}