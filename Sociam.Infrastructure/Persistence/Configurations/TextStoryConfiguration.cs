using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using System.Text.Json;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class TextStoryConfiguration : IEntityTypeConfiguration<TextStory>
{
    public void Configure(EntityTypeBuilder<TextStory> builder)
    {
        builder.Property(s => s.HashTags)
            .HasConversion(
                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default) ?? new List<string>()
            )
            .Metadata.SetValueComparer(
                new ValueComparer<List<string>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            );
    }
}