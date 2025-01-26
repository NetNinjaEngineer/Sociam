using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;

namespace Sociam.Infrastructure.Persistence.Configurations;
internal sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id).ValueGeneratedNever();

        builder.Property(g => g.Name).HasMaxLength(100).IsRequired();

        builder.HasMany(g => g.Members)
           .WithOne(gm => gm.Group)
           .HasForeignKey(gm => gm.GroupId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(g => g.CreatedByUser)
            .WithOne().HasForeignKey<Group>(g => g.CreatedByUserId).IsRequired();

        builder.ToTable("Groups").HasIndex(g => g.CreatedByUserId).IsUnique(false);
    }
}
