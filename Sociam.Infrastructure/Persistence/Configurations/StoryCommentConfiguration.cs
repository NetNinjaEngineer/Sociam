using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class StoryCommentConfiguration : IEntityTypeConfiguration<StoryComment>
{
    public void Configure(EntityTypeBuilder<StoryComment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Comment)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasOne(x => x.Story)
            .WithMany(x => x.StoryComments)
            .HasForeignKey(x => x.StoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.CommentedBy)
            .WithMany()
            .HasForeignKey(x => x.CommentedById)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}