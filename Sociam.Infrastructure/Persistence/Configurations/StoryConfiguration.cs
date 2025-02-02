using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;
internal sealed class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.StoryPrivacy)
            .HasColumnType("VARCHAR")
            .HasConversion(
                sp => sp.ToString(),
                sp => Enum.Parse<StoryPrivacy>(sp)
                );

        builder.HasIndex(s => s.UserId);

        builder.HasDiscriminator<string>("StoryType")
            .HasValue<TextStory>("text")
            .HasValue<MediaStory>("media");

        builder.Property<string>("StoryType")
            .HasColumnType("VARCHAR(5)")
            .IsRequired();

        builder.UseTphMappingStrategy();

        builder.ToTable("Stories");
    }
}
