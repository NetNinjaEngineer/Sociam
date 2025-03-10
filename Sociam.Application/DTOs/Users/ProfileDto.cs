using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Users;

public sealed record ProfileDto
{
    public string Id { get; init; } = "";
    public string UserName { get; init; } = "";
    public string Email { get; init; } = null!;
    public string? FirstName { get; init; } = null;
    public string? LastName { get; init; } = null;
    public DateOnly DateOfBirth { get; init; } = default;
    public Gender Gender { get; init; } = default;
    public string? ProfilePictureUrl { get; init; } = null;
    public string? CoverPhotoUrl { get; init; } = null;
    public string? Bio { get; init; } = null;
    public DateTimeOffset JoinedAt { get; init; } = default;
    public string TimeZoneId { get; init; } = "";
    public long FollowingCount { get; init; } = 0;
    public long FollowersCount { get; init; } = 0;
    public long FriendsCount { get; init; } = 0;
    public long FriendRequestsCount { get; init; } = 0;
    public long FriendRequestsSentCount { get; init; } = 0;
}