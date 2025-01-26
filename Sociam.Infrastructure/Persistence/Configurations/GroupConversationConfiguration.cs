using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;
public sealed class GroupConversationConfiguration : IEntityTypeConfiguration<GroupConversation>
{
    public void Configure(EntityTypeBuilder<GroupConversation> builder)
    {
        builder.HasOne(gc => gc.Group)
           .WithMany(g => g.GroupConversations)
           .HasForeignKey(gc => gc.GroupId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(gc => gc.Messages)
            .WithOne(m => m.GroupConversation)
            .HasForeignKey(m => m.GroupConversationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
