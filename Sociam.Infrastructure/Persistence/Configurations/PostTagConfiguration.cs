using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;

public sealed class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.HasOne(e => e.Post)
            .WithMany(p => p.Tags)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(e => e.TaggedUser)
            .WithMany()
            .HasForeignKey(e => e.TaggedUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.ToTable("PostTags");
    }
}
