using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;

public sealed class PrivateConversation : Conversation
{
    public string SenderUserId { get; set; }
    public string ReceiverUserId { get; set; }
    public ApplicationUser SenderUser { get; set; } = null!;
    public ApplicationUser ReceiverUser { get; set; } = null!;
}
