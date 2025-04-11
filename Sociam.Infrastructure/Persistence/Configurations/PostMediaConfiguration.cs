using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations
{
    public sealed class PostMediaConfiguration : IEntityTypeConfiguration<PostMedia>
    {
        public void Configure(EntityTypeBuilder<PostMedia> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Id).ValueGeneratedNever();

            builder.Property(pm => pm.MediaType)
                .HasConversion(
                    type => type.ToString(),
                    type => Enum.Parse<PostMediaType>(type));

            builder.HasOne(pm => pm.Post)
                .WithMany(p => p.Media)
                .HasForeignKey(pm => pm.PostId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.ToTable("PostMedia");
        }
    }
}
