using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;
public sealed class PrivateConversationConfiguration : IEntityTypeConfiguration<PrivateConversation>
{
    public void Configure(EntityTypeBuilder<PrivateConversation> builder)
    {
        builder.HasOne(pc => pc.SenderUser)
          .WithMany(u => u.PrivateConversationsSent)
          .HasForeignKey(pc => pc.SenderUserId)
          .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pc => pc.ReceiverUser)
            .WithMany(u => u.PrivateConversationsReceived)
            .HasForeignKey(pc => pc.ReceiverUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(pc => new { pc.SenderUserId, pc.ReceiverUserId }).IsUnique();
    }
}
