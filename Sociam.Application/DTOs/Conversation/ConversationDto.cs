using Sociam.Application.DTOs.Messages;

namespace Sociam.Application.DTOs.Conversation;

public sealed class ConversationDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? LastMessageAt { get; set; }
    public string? SenderId { get; set; }
    public string? RecepientId { get; set; }
    public string? Sender { get; set; }
    public string? Recepient { get; set; }
    public Guid? GroupId { get; set; }
    public string ConversationType { get; set; } = null!;
    public IEnumerable<MessageDto> Messages { get; set; } = [];
}
