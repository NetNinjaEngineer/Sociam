using Microsoft.AspNetCore.Identity;
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

        builder.HasData(LoadUsers());

    }

    private static ApplicationUser[] LoadUsers()
    {
        var passwordHasher = new PasswordHasher<ApplicationUser>();

        return
        [
            new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Mohamed",
            LastName = "Ehab",
            UserName = "Moehab@2002",
            Email = "me5260287@gmail.com",
            EmailConfirmed = true,
            NormalizedEmail = "me5260287@gmail.com".ToUpper(),
            NormalizedUserName = "Moehab@2002".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(2002, 1, 1),
            Gender = Gender.Male,
            Bio = "Software Developer and Tech Enthusiast.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Doe",
            UserName = "JohnDoe@123",
            Email = "johndoe@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "johndoe@example.com".ToUpper(),
            NormalizedUserName = "JohnDoe@123".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1990, 5, 15),
            Gender = Gender.Male,
            Bio = "Loves hiking and photography.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Jane",
            LastName = "Smith",
            UserName = "JaneSmith@456",
            Email = "janesmith@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "janesmith@example.com".ToUpper(),
            NormalizedUserName = "JaneSmith@456".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1985, 8, 22),
            Gender = Gender.Female,
            Bio = "Passionate about art and design.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Alice",
            LastName = "Johnson",
            UserName = "AliceJ@789",
            Email = "alicej@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "alicej@example.com".ToUpper(),
            NormalizedUserName = "AliceJ@789".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1995, 3, 10),
            Gender = Gender.Female,
            Bio = "Travel enthusiast and foodie.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Bob",
            LastName = "Brown",
            UserName = "BobBrown@101",
            Email = "bobbrown@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "bobbrown@example.com".ToUpper(),
            NormalizedUserName = "BobBrown@101".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1980, 12, 5),
            Gender = Gender.Male,
            Bio = "Tech entrepreneur and mentor.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Emily",
            LastName = "Davis",
            UserName = "EmilyD@202",
            Email = "emilyd@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "emilyd@example.com".ToUpper(),
            NormalizedUserName = "EmilyD@202".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1992, 7, 18),
            Gender = Gender.Female,
            Bio = "Fitness trainer and health coach.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Michael",
            LastName = "Wilson",
            UserName = "MichaelW@303",
            Email = "michaelw@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "michaelw@example.com".ToUpper(),
            NormalizedUserName = "MichaelW@303".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1988, 9, 25),
            Gender = Gender.Male,
            Bio = "Musician and songwriter.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Sarah",
            LastName = "Miller",
            UserName = "SarahM@404",
            Email = "sarahm@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "sarahm@example.com".ToUpper(),
            NormalizedUserName = "SarahM@404".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1998, 4, 30),
            Gender = Gender.Female,
            Bio = "Book lover and aspiring writer.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "David",
            LastName = "Moore",
            UserName = "DavidM@505",
            Email = "davidm@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "davidm@example.com".ToUpper(),
            NormalizedUserName = "DavidM@505".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1975, 11, 12),
            Gender = Gender.Male,
            Bio = "History buff and teacher.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Laura",
            LastName = "Taylor",
            UserName = "LauraT@606",
            Email = "laurat@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "laurat@example.com".ToUpper(),
            NormalizedUserName = "LauraT@606".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1990, 6, 20),
            Gender = Gender.Female,
            Bio = "Nature lover and environmentalist.",
            CreatedAt = DateTimeOffset.Now
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Chris",
            LastName = "Anderson",
            UserName = "ChrisA@707",
            Email = "chrisa@example.com",
            EmailConfirmed = true,
            NormalizedEmail = "chrisa@example.com".ToUpper(),
            NormalizedUserName = "ChrisA@707".ToUpper(),
            PasswordHash = passwordHasher.HashPassword(null, "P@ssw1234"),
            DateOfBirth = new DateOnly(1985, 2, 14),
            Gender = Gender.Male,
            Bio = "Gamer and tech enthusiast.",
            CreatedAt = DateTimeOffset.Now
        }
        ];
    }
}
