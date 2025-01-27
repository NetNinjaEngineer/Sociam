using Sociam.Domain.Entities;

namespace Sociam.Application.DTOs.Mentions
{
    public sealed class MessageMentionDto
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public string MentionedUserId { get; set; } = null!;
        public string MentionedUser { get; set; } = null!;
        public string MentionType { get; set; } = null!;

        public static MessageMentionDto FromEntity(MessageMention messageMention)
            => new()
            {
                Id = messageMention.Id,
                MessageId = messageMention.MessageId,
                MentionedUserId = messageMention.MentionedUserId,
                MentionedUser = string.Concat(
                    messageMention.MentionedUser?.FirstName, " ", messageMention.MentionedUser?.LastName),
                MentionType = messageMention.MentionType.ToString()
            };
    }
}
