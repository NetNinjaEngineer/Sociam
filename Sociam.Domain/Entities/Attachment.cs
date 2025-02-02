using Sociam.Domain.Entities.common;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;
public sealed class Attachment : BaseEntity
{
    public Guid MessageId { get; set; }
    public Message Message { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Name { get; set; } = null!;
    public long AttachmentSize { get; set; }
    public AttachmentType AttachmentType { get; set; }
}
