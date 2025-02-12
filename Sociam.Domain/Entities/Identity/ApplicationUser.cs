using Microsoft.AspNetCore.Identity;
using Sociam.Domain.Enums;

namespace Sociam.Domain.Entities.Identity;
public sealed class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? CoverPhotoUrl { get; set; }
    public string? Bio { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? Code { get; set; }
    public DateTimeOffset? CodeExpiration { get; set; }
    public string TimeZoneId { get; set; } = string.Empty;
    public List<RefreshToken>? RefreshTokens { get; set; }
    public ICollection<LiveStream> LiveStreams { get; set; } = [];
    public ICollection<Story> Stories { get; set; } = [];
    public ICollection<StoryView> ViewedStories { get; set; } = [];
    public ICollection<UserFollower> Following { get; set; } = [];
    public ICollection<UserFollower> Followers { get; set; } = [];
    public ICollection<Friendship> FriendshipsRequested { get; set; } = [];
    public ICollection<Friendship> FriendshipsReceived { get; set; } = [];
    public ICollection<PrivateConversation> PrivateConversationsSent { get; set; } = [];
    public ICollection<PrivateConversation> PrivateConversationsReceived { get; set; } = [];
    public ICollection<Message> MessagesSent { get; set; } = [];
    public ICollection<Message> MessagesReceived { get; set; } = [];
    public ICollection<JoinGroupRequest> JoinGroupRequests { get; set; } = [];
}