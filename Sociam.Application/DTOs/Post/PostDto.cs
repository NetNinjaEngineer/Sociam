using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Post;

public sealed class PostDto
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public string CreatedById { get; set; } = null!;
    public long SharesCount { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public PostPrivacy Privacy { get; set; }
    public Guid? OriginalPostId { get; set; }
    public PostLocationDto? Location { get; set; }
    public PostFeeling Feeling { get; set; }
}
