using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;
public sealed class LiveStream : BaseEntity
{
    public string Title { get; set; } = null!;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public bool IsLive { get; set; }
    public int ViewerCount { get; set; }
    public string RecordingUrl { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
