using Sociam.Domain.Enums;

namespace Sociam.Application.DTOs.FriendshipRequests;
public sealed class PendingFriendshipRequest
{
    public string SenderId { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string RecipientId { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string FriendRequestId { get; set; } = string.Empty;
    public DateTimeOffset RequestedAt { get; set; }
    public FriendshipStatus Status { get; set; }
    public string SenderProfilePicture { get; set; } = string.Empty;
    public string RecipientProfilePicture { get; set; } = string.Empty;
}
