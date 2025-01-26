namespace Sociam.Application.DTOs.FriendshipRequests;

public sealed record AcceptFriendRequestDto(
    string UserId,
    Guid FriendshipId
);
