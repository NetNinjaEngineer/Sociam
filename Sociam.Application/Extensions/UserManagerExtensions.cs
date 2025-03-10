using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sociam.Application.DTOs.Users;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Application.Extensions;

public static class UserManagerExtensions
{
    public static async Task<ProfileDto?> GetUserProfileAsync(
        this UserManager<ApplicationUser> userManager,
        string userId,
        IConfiguration configuration)
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
                ProfilePictureUrl = !string.IsNullOrEmpty(user.ProfilePictureUrl) ?
                    $"{configuration["BaseApiUrl"]}/Uploads/Images/{user.ProfilePictureUrl}" : null,
                CoverPhotoUrl = !string.IsNullOrEmpty(user.CoverPhotoUrl) ?
                    $"{configuration["BaseApiUrl"]}/Uploads/Images/{user.CoverPhotoUrl}" : null,
                Bio = user.Bio,
                JoinedAt = user.CreatedAt,
                TimeZoneId = user.TimeZoneId,
                FollowingCount = user.Following.LongCount(),
                FollowersCount = user.Followers.LongCount(),
                FriendRequestsCount = user.FriendshipsReceived.LongCount(fr => fr.FriendshipStatus == FriendshipStatus.Pending),
                FriendRequestsSentCount = user.FriendshipsRequested.LongCount(fr => fr.FriendshipStatus != FriendshipStatus.Accepted),
                FriendsCount = user.FriendshipsReceived.LongCount(fr => fr.FriendshipStatus == FriendshipStatus.Accepted) +
                               user.FriendshipsRequested.LongCount(fr => fr.FriendshipStatus == FriendshipStatus.Accepted)
            })
            .FirstOrDefaultAsync();

        return userProfile;
    }
}