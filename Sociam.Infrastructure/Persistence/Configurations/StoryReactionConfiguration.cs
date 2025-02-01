using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class StoryReactionConfiguration : IEntityTypeConfiguration<StoryReaction>
{
    public void Configure(EntityTypeBuilder<StoryReaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.ReactionType)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<ReactionType>(x)
            );

        builder.HasOne(x => x.Story)
            .WithMany(x => x.StoryReactions)
            .HasForeignKey(x => x.StoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.ReactedBy)
            .WithMany()
            .HasForeignKey(x => x.ReactedById)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}