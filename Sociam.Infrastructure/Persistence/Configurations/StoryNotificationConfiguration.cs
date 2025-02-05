using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;
public sealed class StoryNotificationConfiguration : IEntityTypeConfiguration<StoryNotification>
{
    public void Configure(EntityTypeBuilder<StoryNotification> builder)
    {
        builder.HasIndex(x => x.StoryId);

        builder.ToTable("StoryNotifications");
    }
}
