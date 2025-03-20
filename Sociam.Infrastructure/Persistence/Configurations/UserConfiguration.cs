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
                Id = "702C7401-F83C-4684-9421-9AA74FC40050",
                FirstName = "Mohamed",
                LastName = "Ehab",
                UserName = "Moehab@2002",
                Email = "me5260287@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "me5260287@gmail.com".ToUpper(),
                NormalizedUserName = "Moehab@2002".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null!, "P@ssw1234"),
                DateOfBirth = new DateOnly(2002, 1, 1),
                Gender = Gender.Male,
                Bio = "Software Developer and Tech Enthusiast.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "83DFC31A-11E5-4AD3-955D-10766FCAA955",
                FirstName = "Mohamed",
                LastName = "Ehab",
                UserName = "memo@2002",
                Email = "melhelaly43@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "melhelaly43@gmail.com".ToUpper(),
                NormalizedUserName = "memo@2002".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null!, "P@ssw1234"),
                DateOfBirth = new DateOnly(2002, 12, 10),
                Gender = Gender.Male,
                Bio = "Software Developer and Tech Enthusiast.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "3EB45CDA-F2EE-43E7-B9F1-D52562E05929",
                FirstName = "John",
                LastName = "Doe",
                UserName = "JohnDoe@123",
                Email = "johndoe@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "johndoe@example.com".ToUpper(),
                NormalizedUserName = "JohnDoe@123".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1990, 5, 15),
                Gender = Gender.Male,
                Bio = "Loves hiking and photography.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "9818FAE0-A167-4808-A30D-BC7418A53CB0",
                FirstName = "Jane",
                LastName = "Smith",
                UserName = "JaneSmith@456",
                Email = "janesmith@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "janesmith@example.com".ToUpper(),
                NormalizedUserName = "JaneSmith@456".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1985, 8, 22),
                Gender = Gender.Female,
                Bio = "Passionate about art and design.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "FE2FB445-6562-49DD-B0A3-77E0A3A1C376",
                FirstName = "Alice",
                LastName = "Johnson",
                UserName = "AliceJ@789",
                Email = "alicej@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "alicej@example.com".ToUpper(),
                NormalizedUserName = "AliceJ@789".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1995, 3, 10),
                Gender = Gender.Female,
                Bio = "Travel enthusiast and foodie.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "0821819C-64AE-4C73-96F2-4E607AA59D7E",
                FirstName = "Bob",
                LastName = "Brown",
                UserName = "BobBrown@101",
                Email = "bobbrown@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "bobbrown@example.com".ToUpper(),
                NormalizedUserName = "BobBrown@101".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1980, 12, 5),
                Gender = Gender.Male,
                Bio = "Tech entrepreneur and mentor.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "5B91855C-2D98-4E2B-B919-CDE322C9002D",
                FirstName = "Emily",
                LastName = "Davis",
                UserName = "EmilyD@202",
                Email = "emilyd@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "emilyd@example.com".ToUpper(),
                NormalizedUserName = "EmilyD@202".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1992, 7, 18),
                Gender = Gender.Female,
                Bio = "Fitness trainer and health coach.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "5326BB55-A26F-47FE-ABC4-9DF44F7B0333",
                FirstName = "Michael",
                LastName = "Wilson",
                UserName = "MichaelW@303",
                Email = "michaelw@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "michaelw@example.com".ToUpper(),
                NormalizedUserName = "MichaelW@303".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1988, 9, 25),
                Gender = Gender.Male,
                Bio = "Musician and songwriter.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "B3945AB7-1F46-4829-9DEA-6860E283582F",
                FirstName = "Sarah",
                LastName = "Miller",
                UserName = "SarahM@404",
                Email = "sarahm@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "sarahm@example.com".ToUpper(),
                NormalizedUserName = "SarahM@404".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1998, 4, 30),
                Gender = Gender.Female,
                Bio = "Book lover and aspiring writer.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "3944C201-0184-4F97-83A6-B6E4852C961F",
                FirstName = "David",
                LastName = "Moore",
                UserName = "DavidM@505",
                Email = "davidm@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "davidm@example.com".ToUpper(),
                NormalizedUserName = "DavidM@505".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1975, 11, 12),
                Gender = Gender.Male,
                Bio = "History buff and teacher.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "0A9232F3-BC6D-4610-AAFF-F1032831E847",
                FirstName = "Laura",
                LastName = "Taylor",
                UserName = "LauraT@606",
                Email = "laurat@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "laurat@example.com".ToUpper(),
                NormalizedUserName = "LauraT@606".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1990, 6, 20),
                Gender = Gender.Female,
                Bio = "Nature lover and environmentalist.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            },
            new ApplicationUser()
            {
                Id = "049759F5-3AD8-46BF-89EE-AC51F3BEED88",
                FirstName = "Chris",
                LastName = "Anderson",
                UserName = "ChrisA@707",
                Email = "chrisa@example.com",
                EmailConfirmed = true,
                NormalizedEmail = "chrisa@example.com".ToUpper(),
                NormalizedUserName = "ChrisA@707".ToUpper(),
                PasswordHash = passwordHasher.HashPassword(null !, "P@ssw1234"),
                DateOfBirth = new DateOnly(1985, 2, 14),
                Gender = Gender.Male,
                Bio = "Gamer and tech enthusiast.",
                CreatedAt = DateTimeOffset.Now,
                TimeZoneId = "Egypt Standard Time"
            }
        ];
    }
}
