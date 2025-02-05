using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class GroupNotificationConfiguration : IEntityTypeConfiguration<GroupNotification>
{
    public void Configure(EntityTypeBuilder<GroupNotification> builder)
    {
        builder.HasIndex(x => x.GroupId);

        builder.ToTable("GroupNotifications");
    }
}