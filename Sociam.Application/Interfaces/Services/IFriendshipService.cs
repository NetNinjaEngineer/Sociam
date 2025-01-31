using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.Features.FriendRequests.Commands.CurrentUserAcceptFriendRequest;
using Sociam.Application.Features.FriendRequests.Commands.CurrentUserSendFriendRequest;
using Sociam.Application.Features.FriendRequests.Queries.AreFriendsForCurrentUser;
using Sociam.Application.Features.FriendRequests.Queries.CheckIfAreFriends;

namespace Sociam.Application.Interfaces.Services;
public interface IFriendshipService
{
    Task<Result<FriendshipResponseDto>> SendFriendRequestAsync(SendFriendshipRequestDto sendFriendshipRequest);

    Task<Result<FriendshipResponseDto>> AcceptFriendRequestAsync(AcceptFriendRequestDto acceptFriendRequest);

    Task<Result<IEnumerable<PendingFriendshipRequest>>> GetCurrentUserSentFriendRequestsAsync();
    Task<Result<IEnumerable<PendingFriendshipRequest>>> GetCurrentUserReceivedFriendRequestsAsync();

    Task<Result<IEnumerable<GetUserAcceptedFriendshipDto>>> GetLoggedInUserAcceptedFriendshipsAsync();

    Task<Result<bool>> AreFriendsAsync(CheckIfAreFriendsQuery query);

    Task<Result<bool>> AreFriendsWithCurrentUserAsync(AreFriendsForCurrentUserQuery query);

    Task<Result<FriendshipResponseDto>> SendFriendRequestCurrentUserAsync(CurrentUserSendFriendRequestCommand command);

    Task<Result<FriendshipResponseDto>> CurrentUserAcceptFriendRequestAsync(CurrentUserAcceptFriendRequestCommand command);

}
