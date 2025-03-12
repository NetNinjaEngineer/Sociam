using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;
internal sealed class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.HasDiscriminator<string>("ConversationType")
            .HasValue<PrivateConversation>("Private")
            .HasValue<GroupConversation>("Group");
    }
}
