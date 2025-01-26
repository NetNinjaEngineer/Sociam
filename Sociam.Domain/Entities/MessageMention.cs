using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class MessageMention : BaseEntity
{
    public Guid MessageId { get; set; }
    public Message Message { get; set; } = null!;
    public string MentionedUserId { get; set; } = null!;
    public ApplicationUser MentionedUser { get; set; } = null!;
    public MentionType MentionType { get; set; }
}