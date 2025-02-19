using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;

internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => x.RecipientId);

        builder.HasIndex(x => x.ActorId);

        builder.Property(x => x.Type)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<NotificationType>(x));

        builder.Property(x => x.Status)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<NotificationStatus>(x));

        builder.HasOne(x => x.Actor)
            .WithMany()
            .HasForeignKey(x => x.ActorId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(x => x.Recipient)
            .WithMany()
            .HasForeignKey(x => x.RecipientId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasDiscriminator<string>("NKind")
            .HasValue<PostNotification>("post")
            .HasValue<NetworkNotification>("network")
            .HasValue<GroupNotification>("group")
            .HasValue<MediaNotification>("media")
            .HasValue<StoryNotification>("story");

        builder.UseTphMappingStrategy();

        builder.ToTable("Notifications");
    }
}