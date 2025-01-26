using Sociam.Domain.Entities;

namespace Sociam.Application.DTOs.Reactions;

public sealed class MessageReactionDto
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public string ReactedById { get; set; } = null!;
    public string ReactedBy { get; set; } = null!;
    public string Reaction { get; set; } = null!;

    public static MessageReactionDto FromEntity(MessageReaction messageReaction)
        => new()
        {
            Id = messageReaction.Id,
            MessageId = messageReaction.MessageId,
            ReactedById = messageReaction.UserId,
            ReactedBy = string.Concat(
                messageReaction.User.FirstName, " ", messageReaction.User.LastName),
            Reaction = messageReaction.ReactionType.ToString()
        };
}
