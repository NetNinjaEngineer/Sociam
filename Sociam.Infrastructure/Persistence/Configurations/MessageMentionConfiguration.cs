using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class MessageMentionConfiguration : IEntityTypeConfiguration<MessageMention>
{
    public void Configure(EntityTypeBuilder<MessageMention> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.HasOne(e => e.Message)
            .WithMany(e => e.Mentions)
            .HasForeignKey(e => e.MessageId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(e => e.MentionedUser)
            .WithMany()
            .HasForeignKey(e => e.MentionedUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(e => e.MentionType)
            .HasConversion(
                builder => builder.ToString(),
                value => (MentionType)Enum.Parse(typeof(MentionType), value));

        builder.ToTable("MessageMentions");
    }
}
