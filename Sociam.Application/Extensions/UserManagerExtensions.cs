using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sociam.Application.DTOs.Users;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Application.Extensions;

public static class UserManagerExtensions
{
    public static async Task<ProfileDto?> GetUserProfileAsync(this UserManager<ApplicationUser> userManager, string userId)
    {
        var userProfile = await userManager.Users
            .AsNoTracking()
            .Include(user => user.Followers)
            .Include(user => user.Following)
            .Include(user => user.FriendshipsReceived)
            .Include(user => user.FriendshipsRequested)
            .Where(user => user.Id == userId)
            .Select(user => new ProfileDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                ProfilePictureUrl = user.ProfilePictureUrl,
                CoverPhotoUrl = user.CoverPhotoUrl,
                Bio = user.Bio,
                JoinedAt = user.CreatedAt,
                TimeZoneId = user.TimeZoneId,
                FollowingCount = user.Following.LongCount(),
                FollowersCount = user.Followers.LongCount(),
                FriendRequestsCount = user.FriendshipsReceived.LongCount(fr => fr.FriendshipStatus != FriendshipStatus.Accepted),
                FriendRequestsSentCount = user.FriendshipsRequested.LongCount(fr => fr.FriendshipStatus != FriendshipStatus.Accepted),
                FriendsCount = user.FriendshipsReceived.LongCount(fr => fr.FriendshipStatus == FriendshipStatus.Accepted) +
                               user.FriendshipsRequested.LongCount(fr => fr.FriendshipStatus == FriendshipStatus.Accepted)
            })
            .FirstOrDefaultAsync();

        return userProfile;
    }
}