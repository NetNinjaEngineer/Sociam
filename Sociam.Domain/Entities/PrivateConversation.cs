using Sociam.Domain.Entities.Identity;

namespace Sociam.Domain.Entities;

public sealed class PrivateConversation : Conversation
{
    public string SenderUserId { get; set; } = null!;
    public string ReceiverUserId { get; set; } = null!;
    public ApplicationUser SenderUser { get; set; } = null!;
    public ApplicationUser ReceiverUser { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = [];

}
