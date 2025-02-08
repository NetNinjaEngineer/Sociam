using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class MediaStoryConfiguration : IEntityTypeConfiguration<MediaStory>
{
    public void Configure(EntityTypeBuilder<MediaStory> builder)
    {
        builder.Property(s => s.MediaUrl)
            .IsRequired();

        builder.Property(s => s.MediaType)
            .HasConversion(
                m => m.ToString(),
                m => Enum.Parse<MediaType>(m));

        builder.Property(s => s.Caption)
            .HasMaxLength(500);
    }
}