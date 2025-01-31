using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.FriendshipRequests;
using Sociam.Application.DTOs.Users;
using Sociam.Application.Features.FriendRequests.Commands.CurrentUserAcceptFriendRequest;
using Sociam.Application.Features.FriendRequests.Commands.CurrentUserSendFriendRequest;
using Sociam.Application.Features.FriendRequests.Commands.RejectFriendRequest;
using Sociam.Application.Features.FriendRequests.Queries.AreFriendsForCurrentUser;
using Sociam.Application.Features.FriendRequests.Queries.CheckIfAreFriends;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Specifications;
using System.Net;

namespace Sociam.Services.Services;
public sealed class FriendshipService(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser,
    IMapper mapper,
    IHubContext<FriendRequestHub> hubContext) : IFriendshipService
{
    public async Task<Result<FriendshipResponseDto>> SendFriendRequestAsync(
        SendFriendshipRequestDto sendFriendshipRequest)
    {
        var requester = await userManager.FindByIdAsync(sendFriendshipRequest.RequesterId);

        var addressee = await userManager.FindByIdAsync(sendFriendshipRequest.AddresseeId);

        if (requester is null || addressee is null)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.NotFound,
                DomainErrors.Users.UserNotExists);

        if (sendFriendshipRequest.AddresseeId == sendFriendshipRequest.RequesterId)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.Conflict,
                DomainErrors.Friendship.CanNotSendFriendRequestToYourSelf);


        var existingFriendship = await unitOfWork.FriendshipRepository
            .GetFriendshipAsync(sendFriendshipRequest.RequesterId, sendFriendshipRequest.AddresseeId);

        if (existingFriendship is not null)
        {
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.BadRequest,
                existingFriendship.FriendshipStatus switch
                {
                    FriendshipStatus.Pending => DomainErrors.Friendship.PendingFriendRequest,
                    FriendshipStatus.Accepted => DomainErrors.Friendship.AlreadyAcceptedFriendRequest,
                    FriendshipStatus.Blocked => DomainErrors.Friendship.BlockedFriendRequest,
                    FriendshipStatus.Rejected => DomainErrors.Friendship.RejectedFriendRequest,
                    _ => DomainErrors.Friendship.UndefindFriendRequestStatus
                });
        }

        var friendShip = new Friendship
        {
            Id = Guid.NewGuid(),
            RequesterId = sendFriendshipRequest.RequesterId,
            ReceiverId = sendFriendshipRequest.AddresseeId,
            FriendshipStatus = FriendshipStatus.Pending,
            UpdatedAt = DateTimeOffset.Now
        };

        unitOfWork.Repository<Friendship>()?.Create(friendShip);

        await unitOfWork.SaveChangesAsync();

        var fShip =
            await unitOfWork.FriendshipRepository.GetBySpecificationAndIdAsync(
                new FriendshipRequestWithRequesterAndReceiverSpecification(), friendShip.Id);

        if (fShip == null)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.BadRequest,
                DomainErrors.Friendship.UnableToCreateFriendRequest);

        var myFriend = string.Concat(fShip.Requester.FirstName, " ", fShip.Requester.LastName);

        await SendReceiveNewFriendRequestNotificationAsync(fShip.ReceiverId, $"{myFriend} sent you a friend request");

        return Result<FriendshipResponseDto>.Success(
            new FriendshipResponseDto(
                myFriend,
                string.Concat(fShip.Receiver.FirstName, " ", fShip.Receiver.LastName),
                fShip.FriendshipStatus.ToString(),
                fShip.CreatedAt,
                fShip.UpdatedAt
            ));

    }

    public async Task<Result<FriendshipResponseDto>> AcceptFriendRequestAsync(AcceptFriendRequestDto acceptFriendRequest)
    {
        var friendship = await unitOfWork.FriendshipRepository.GetByIdAsync(acceptFriendRequest.FriendshipId);

        if (friendship == null)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.NotFound,
                DomainErrors.Friendship.NotFoundFriendRequest);

        if (string.IsNullOrEmpty(acceptFriendRequest.UserId))
            return Result<FriendshipResponseDto>.Failure(HttpStatusCode.BadRequest, "UserId Can Not Be Empty !!!");

        if (friendship.ReceiverId != acceptFriendRequest.UserId)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.Unauthorized,
                DomainErrors.Friendship.UnauthorizedToAcceptFriendRequest);

        if (friendship.FriendshipStatus != FriendshipStatus.Pending)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.BadRequest, DomainErrors.Friendship.FriendRequestMustBePending);

        friendship.FriendshipStatus = FriendshipStatus.Accepted;
        friendship.UpdatedAt = DateTimeOffset.Now;

        unitOfWork.FriendshipRepository.Update(friendship);

        await unitOfWork.SaveChangesAsync();

        var fShip = await unitOfWork.FriendshipRepository
            .GetBySpecificationAndIdAsync(
                new FriendshipRequestWithRequesterAndReceiverSpecification(),
                friendship.Id);

        if (fShip is not null)
        {
            var myFriend = string.Concat(fShip.Receiver.FirstName, " ", fShip.Receiver.LastName);
            await SendAcceptFriendRequestNotificationAsync(fShip.RequesterId,
                $"{myFriend} accepted your friend request");

            return Result<FriendshipResponseDto>.Success(new FriendshipResponseDto(
                string.Concat(fShip.Requester.FirstName, " ", fShip.Requester.LastName),
                myFriend,
                fShip.FriendshipStatus.ToString(),
                fShip.CreatedAt,
                fShip.UpdatedAt
            ));
        }

        return Result<FriendshipResponseDto>.Failure(HttpStatusCode.BadRequest, DomainErrors.Friendship.NotFoundFriendRequest);
    }

    public async Task<Result<IEnumerable<PendingFriendshipRequest>>> GetCurrentUserSentFriendRequestsAsync()
    {
        var pendingRequests = await unitOfWork.FriendshipRepository.GetSentFriendRequestsAsync(currentUser.Id);
        var mappedPendingRequests = mapper.Map<IEnumerable<PendingFriendshipRequest>>(pendingRequests);
        return Result<IEnumerable<PendingFriendshipRequest>>.Success(mappedPendingRequests);
    }

    public async Task<Result<IEnumerable<PendingFriendshipRequest>>> GetCurrentUserReceivedFriendRequestsAsync()
    {
        var pendingRequests = await unitOfWork.FriendshipRepository.GetReceivedFriendRequestsAsync(currentUser.Id);
        var mappedPendingRequests = mapper.Map<IEnumerable<PendingFriendshipRequest>>(pendingRequests);
        return Result<IEnumerable<PendingFriendshipRequest>>.Success(mappedPendingRequests);
    }

    public async Task<Result<IEnumerable<GetUserAcceptedFriendshipDto>>> GetLoggedInUserAcceptedFriendshipsAsync()
    {
        var acceptedFriendships = await unitOfWork.FriendshipRepository.GetAcceptedFriendshipsForUserAsync(currentUser.Id);
        var mappedAcceptedFriendships = mapper.Map<IEnumerable<GetUserAcceptedFriendshipDto>>(acceptedFriendships);
        return Result<IEnumerable<GetUserAcceptedFriendshipDto>>.Success(mappedAcceptedFriendships);
    }

    public async Task<Result<bool>> AreFriendsAsync(CheckIfAreFriendsQuery query)
        => Result<bool>.Success(await unitOfWork.FriendshipRepository.AreFriendsAsync(query.User1Id, query.User2Id));

    public async Task<Result<bool>> AreFriendsWithCurrentUserAsync(AreFriendsForCurrentUserQuery query)
        => query.FriendId == currentUser.Id ?
            Result<bool>.Failure(HttpStatusCode.Conflict, message: DomainErrors.Friendship.CanNotBeFriendOfYourself) :
            Result<bool>.Success(await unitOfWork.FriendshipRepository.AreFriendsAsync(currentUser.Id, query.FriendId));

    public async Task<Result<FriendshipResponseDto>> SendFriendRequestCurrentUserAsync(
        CurrentUserSendFriendRequestCommand command)
    {
        var requester = await userManager.FindByIdAsync(currentUser.Id);

        var addressee = await userManager.FindByIdAsync(command.FriendId);

        if (requester is null || addressee is null)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.NotFound,
                DomainErrors.Users.UserNotExists);

        if (command.FriendId == currentUser.Id)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.Conflict,
                DomainErrors.Friendship.CanNotSendFriendRequestToYourSelf);

        var existingFriendship = await unitOfWork.FriendshipRepository
            .GetFriendshipAsync(command.FriendId, currentUser.Id);

        if (existingFriendship is not null)
        {
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.BadRequest,
                existingFriendship.FriendshipStatus switch
                {
                    FriendshipStatus.Pending => DomainErrors.Friendship.PendingFriendRequest,
                    FriendshipStatus.Accepted => DomainErrors.Friendship.AlreadyAcceptedFriendRequest,
                    FriendshipStatus.Blocked => DomainErrors.Friendship.BlockedFriendRequest,
                    FriendshipStatus.Rejected => DomainErrors.Friendship.RejectedFriendRequest,
                    _ => DomainErrors.Friendship.UndefindFriendRequestStatus
                });
        }

        var friendShip = new Friendship
        {
            Id = Guid.NewGuid(),
            RequesterId = currentUser.Id,
            ReceiverId = command.FriendId,
            FriendshipStatus = FriendshipStatus.Pending,
            UpdatedAt = DateTimeOffset.Now
        };

        unitOfWork.Repository<Friendship>()?.Create(friendShip);

        await unitOfWork.SaveChangesAsync();

        var fShip =
            await unitOfWork.FriendshipRepository.GetBySpecificationAndIdAsync(
                new FriendshipRequestWithRequesterAndReceiverSpecification(), friendShip.Id);

        if (fShip == null)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.BadRequest,
                DomainErrors.Friendship.UnableToCreateFriendRequest);

        var senderRequest = string.Concat(fShip.Requester.FirstName, " ", fShip.Requester.LastName);

        await SendReceiveNewFriendRequestNotificationAsync(fShip.ReceiverId, $"{senderRequest} sent you a friend request");

        return Result<FriendshipResponseDto>.Success(
            new FriendshipResponseDto(
                senderRequest,
                string.Concat(fShip.Receiver.FirstName, " ", fShip.Receiver.LastName),
                fShip.FriendshipStatus.ToString(),
                fShip.CreatedAt,
                fShip.UpdatedAt
            ));

    }

    private async Task SendReceiveNewFriendRequestNotificationAsync(string fShipReceiverId, string notificationMessage)
    {
        await hubContext.Clients.User(fShipReceiverId).SendAsync("ReceivedNewFriendRequest", notificationMessage);
    }

    public async Task<Result<FriendshipResponseDto>> CurrentUserAcceptFriendRequestAsync(
        CurrentUserAcceptFriendRequestCommand command)
    {
        var friendship = await unitOfWork.FriendshipRepository.GetByIdAsync(command.FriendshipId);

        if (friendship == null)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.NotFound,
                DomainErrors.Friendship.NotFoundFriendRequest);

        if (friendship.ReceiverId != currentUser.Id)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.Unauthorized,
                DomainErrors.Friendship.UnauthorizedToAcceptFriendRequest);

        if (friendship.FriendshipStatus != FriendshipStatus.Pending)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.BadRequest, DomainErrors.Friendship.FriendRequestMustBePending);

        friendship.FriendshipStatus = FriendshipStatus.Accepted;
        friendship.UpdatedAt = DateTimeOffset.Now;

        unitOfWork.FriendshipRepository.Update(friendship);

        await unitOfWork.SaveChangesAsync();


        var fShip = await unitOfWork.FriendshipRepository
            .GetBySpecificationAndIdAsync(
                new FriendshipRequestWithRequesterAndReceiverSpecification(),
                friendship.Id);

        if (fShip is null)
            return Result<FriendshipResponseDto>.Failure(
                HttpStatusCode.BadRequest,
                DomainErrors.Friendship.NotFoundFriendRequest);

        var myFriend = string.Concat(fShip.Receiver.FirstName, " ", fShip.Receiver.LastName);

        await SendAcceptFriendRequestNotificationAsync(fShip.RequesterId, $"{myFriend} accepted your friend request");

        return Result<FriendshipResponseDto>.Success(
            new FriendshipResponseDto(
                string.Concat(fShip.Requester.FirstName, " ", fShip.Requester.LastName),
                myFriend,
                fShip.FriendshipStatus.ToString(),
                fShip.CreatedAt,
                fShip.UpdatedAt));

    }

    public async Task<Result<bool>> RejectFriendRequestAsync(RejectFriendRequestCommand command)
    {
        // Check is There exists a friend request with this id
        var friendship = await unitOfWork.FriendshipRepository.GetByIdAsync(command.FriendRequestId);

        if (friendship is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, DomainErrors.Friendship.NotFoundFriendRequest);

        // The FriendRequest is rejected by the receiver that received the request
        // We need to authorize that the receiver is the only that can reject not anyone else

        if (friendship.ReceiverId != currentUser.Id)
            return Result<bool>.Failure(
                statusCode: HttpStatusCode.Unauthorized,
                error: DomainErrors.Friendship.UnauthorizedToRejectFriendRequest
                );

        if (friendship.FriendshipStatus != FriendshipStatus.Pending)
            return Result<bool>.Failure(
                statusCode: HttpStatusCode.BadRequest,
                error: string.Format(DomainErrors.Friendship.CanNotRejectFriendRequest, friendship.FriendshipStatus.ToString())
                );

        friendship.FriendshipStatus = FriendshipStatus.Rejected;
        friendship.UpdatedAt = DateTimeOffset.Now;

        await unitOfWork.SaveChangesAsync();

        // send a realtime notification to the requestor that the receiver rejected his request
        await hubContext.Clients.User(friendship.RequesterId)
            .SendAsync("FriendRequestRejected",
                $"{currentUser.FullName} rejected your friend request");

        return Result<bool>.Success(true);

    }

    public async Task<Result<List<UserProfileDto>>> GetFriendsAsync()
    {
        var friends = await unitOfWork.FriendshipRepository.GetFriendsOfUserAsync(currentUser.Id);
        var mappedResults = mapper.Map<List<UserProfileDto>>(friends);
        return Result<List<UserProfileDto>>.Success(mappedResults);
    }

    private async Task SendAcceptFriendRequestNotificationAsync(string fShipRequesterId, string notificationMessage)
    {
        await hubContext.Clients.User(fShipRequesterId).SendAsync("FriendRequestAccepted", notificationMessage);
    }
}
