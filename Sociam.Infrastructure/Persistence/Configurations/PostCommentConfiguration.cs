using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
{
    public void Configure(EntityTypeBuilder<PostComment> builder)
    {
        builder.HasKey(comment => comment.Id);
        builder.Property(comment => comment.Id).ValueGeneratedNever();

        builder.HasOne(comment => comment.Post)
            .WithMany(post => post.Comments)
            .HasForeignKey(comment => comment.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(comment => comment.CreatedBy)
            .WithMany()
            .HasForeignKey(comment => comment.CreatedById)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(pc => pc.ParentComment)
            .WithMany(pc => pc.Replies)
            .HasForeignKey(pc => pc.ParentCommentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("PostComments");
    }
}