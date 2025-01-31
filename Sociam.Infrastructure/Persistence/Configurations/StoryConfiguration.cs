using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using System.Text.Json;

namespace Sociam.Infrastructure.Persistence.Configurations;
internal sealed class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.MediaUrl)
            .IsRequired();

        builder.Property(s => s.MediaType)
            .HasConversion(
                m => m.ToString(),
                m => Enum.Parse<MediaType>(m));

        builder.Property(s => s.Caption)
            .HasMaxLength(500);

        builder.Property(s => s.HashTags)
            .HasConversion(
                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default) ?? new List<string>()
            );

        builder.HasIndex(s => s.UserId);

        builder.ToTable("Stories");
    }
}
