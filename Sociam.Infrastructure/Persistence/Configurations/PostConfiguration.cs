using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;
public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(post => post.Id);
        builder.Property(post => post.Id).ValueGeneratedNever();

        builder.Property(post => post.Text).IsRequired(false);

        builder.HasOne(post => post.CreatedBy)
            .WithMany(user => user.Posts)
            .HasForeignKey(post => post.CreatedById)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(post => post.OriginalPost)
            .WithMany()
            .HasForeignKey(post => post.OriginalPostId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Property(post => post.Feeling)
            .HasConversion(
                post => post.ToString(),
                post => Enum.Parse<PostFeeling>(post));

        builder.Property(post => post.Privacy)
            .HasConversion(
                post => post.ToString(),
                post => Enum.Parse<PostPrivacy>(post));

        builder.OwnsOne(post => post.Location, options =>
        {
            options.Property(location => location.City)
                .HasColumnName("City")
                .IsRequired(false);

            options.Property(location => location.Country)
                .HasColumnName("Country")
                .IsRequired(false);

            options.Property(location => location.Latitude)
                .HasColumnType("double precision")
                .HasColumnName("Latitude")
                .IsRequired(false);


            options.Property(location => location.Longitude)
                .HasColumnType("double precision")
                .HasColumnName("Longitude")
                .IsRequired(false);

        });

        builder.ToTable("Posts");
    }
}
