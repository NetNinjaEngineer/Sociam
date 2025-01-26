using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();

        builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();

        builder.Property(x => x.ProfilePictureUrl).IsRequired(false);

        builder.Property(x => x.CoverPhotoUrl).IsRequired(false);

        builder.Property(x => x.Bio).HasMaxLength(255).IsRequired(false);

        builder.Property(x => x.Gender).HasConversion(
            g => g.ToString(),
            g => (Gender)Enum.Parse(typeof(Gender), g));

        builder.HasMany(u => u.FriendshipsRequested)
            .WithOne(f => f.Requester).HasForeignKey(f => f.RequesterId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.FriendshipsReceived)
            .WithOne(f => f.Receiver).HasForeignKey(f => f.ReceiverId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Stories)
            .WithOne(s => s.User).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.ViewedStories)
            .WithOne(sv => sv.Viewer)
            .HasForeignKey(sv => sv.ViewerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.LiveStreams)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Following)
            .WithOne(uf => uf.FollowerUser)
            .HasForeignKey(uf => uf.FollowerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Followers)
            .WithOne(uf => uf.FollowedUser)
            .HasForeignKey(uf => uf.FollowedUserId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
