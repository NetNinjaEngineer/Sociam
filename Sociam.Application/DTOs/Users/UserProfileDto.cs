using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.Users;
public sealed class UserProfileDto
{
    public string Id { get; set; } = null!;
    public string? UserName { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? CoverPhotoUrl { get; set; }
    public string? Bio { get; set; }
    public DateTimeOffset JoinedAt { get; set; }
}
