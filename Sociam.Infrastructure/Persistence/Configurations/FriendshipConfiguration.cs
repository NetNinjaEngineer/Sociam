using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;

namespace Sociam.Infrastructure.Persistence.Configurations;
internal sealed class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id).ValueGeneratedNever();

        builder.Property(f => f.FriendshipStatus)
            .HasConversion(
                fs => fs.ToString(),
                fs => (FriendshipStatus)Enum.Parse(typeof(FriendshipStatus), fs));

        builder.ToTable("Friendships");
    }
}
