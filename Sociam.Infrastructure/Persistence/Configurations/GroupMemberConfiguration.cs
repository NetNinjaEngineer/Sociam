using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;
internal sealed class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
{
    public void Configure(EntityTypeBuilder<GroupMember> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.Group)
            .WithMany(g => g.Members)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Property(x => x.Role)
            .HasConversion(
                gm => gm.ToString(),
                gm => Enum.Parse<GroupMemberRole>(gm.ToString()));

        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<GroupMember>(gm => gm.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.AddedBy)
            .WithOne().HasForeignKey<GroupMember>(gm => gm.AddedById)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasIndex(g => g.UserId).IsUnique(false);

        builder.HasIndex(g => g.AddedById).IsUnique(false);

        builder.ToTable("GroupMembers");

    }
}
