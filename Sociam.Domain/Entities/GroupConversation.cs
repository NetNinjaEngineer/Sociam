namespace Sociam.Domain.Entities;

public sealed class GroupConversation : Conversation
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = [];

}