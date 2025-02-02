using Sociam.Domain.Entities.common;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities;

public sealed class MessageReaction : BaseEntity
{
    public Guid MessageId { get; set; }
    public Message Message { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public ReactionType ReactionType { get; set; }
}