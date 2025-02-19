using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class MediaNotificationConfiguration : IEntityTypeConfiguration<MediaNotification>
{
    public void Configure(EntityTypeBuilder<MediaNotification> builder)
    {
        builder.HasIndex(x => x.MediaId);

        builder.Property(x => x.MediaNotificationType)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<MediaNotificationType>(x));
    }
}