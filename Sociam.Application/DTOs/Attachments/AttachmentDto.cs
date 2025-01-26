using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Attachments;
public sealed class AttachmentDto
{
    public string Url { get; set; } = string.Empty;
    public double Size { get; set; }
    public AttachmentType Type { get; set; }

    public static AttachmentDto FromEntity(Attachment attachment)
        => new()
        {
            Url = attachment.Url,
            Size = attachment.AttachmentSize,
            Type = attachment.AttachmentType
        };

}