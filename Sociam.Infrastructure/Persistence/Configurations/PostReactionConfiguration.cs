using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class PostReactionConfiguration : IEntityTypeConfiguration<PostReaction>
{
    public void Configure(EntityTypeBuilder<PostReaction> builder)
    {
        builder.HasKey(postReaction => postReaction.Id);
        builder.Property(postReaction => postReaction.Id).ValueGeneratedNever();

        builder.Property(postReaction => postReaction.Type)
            .HasConversion(
                type => type.ToString(),
                type => Enum.Parse<ReactionType>(type));

        builder.HasOne(postReaction => postReaction.ReactedBy)
            .WithMany()
            .HasForeignKey(postReaction => postReaction.ReactedById)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(postReaction => postReaction.Post)
            .WithMany(post => post.Reactions)
            .HasForeignKey(postReaction => postReaction.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.ToTable("PostReactions");
    }
}