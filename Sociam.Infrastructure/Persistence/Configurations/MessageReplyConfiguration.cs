using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class MessageReplyConfiguration : IEntityTypeConfiguration<MessageReply>
{
    public void Configure(EntityTypeBuilder<MessageReply> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.HasOne(r => r.OriginalMessage)
            .WithMany(m => m.Replies)
            .HasForeignKey(r => r.OriginalMessageId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(r => r.ParentReply)
            .WithMany(r => r.ChildReplies)
            .HasForeignKey(r => r.ParentReplyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.RepliedBy)
            .WithMany()
            .HasForeignKey(r => r.RepliedById)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(r => r.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.HasIndex(r => r.OriginalMessageId);
        builder.HasIndex(r => r.ParentReplyId);
        builder.HasIndex(r => r.CreatedAt);

        builder.Property(r => r.ReplyStatus)
            .HasConversion(
                v => v.ToString(),
                v => (ReplyStatus)Enum.Parse(typeof(ReplyStatus), v)
            );

        builder.ToTable("MessageReplies");
    }
}
