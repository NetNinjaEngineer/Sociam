using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Authorization;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.AddStoryComment;
using Sociam.Application.Features.Stories.Commands.AddStoryReaction;
using Sociam.Application.Features.Stories.Commands.ChangeStoryPrivacy;
using Sociam.Application.Features.Stories.Commands.CreateMediaStory;
using Sociam.Application.Features.Stories.Commands.CreateTextStory;
using Sociam.Application.Features.Stories.Commands.DeleteStory;
using Sociam.Application.Features.Stories.Commands.MarkAsViewed;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
using Sociam.Application.Features.Stories.Queries.GetStoriesByParams;
using Sociam.Application.Features.Stories.Queries.GetStoryById;
using Sociam.Application.Features.Stories.Queries.GetStoryViewers;
using Sociam.Application.Features.Stories.Queries.GetUserStories;
using Sociam.Application.Features.Stories.Queries.HasUnseenStories;
using Sociam.Application.Features.Stories.Queries.IsStoryViewed;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Specifications;
using Sociam.Domain.Utils;
using Sociam.Infrastructure.Persistence;
using System.Net;

namespace Sociam.Services.Services;
public sealed class StoryService(
    IMapper mapper,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork,
    IFileService fileService,
    IHubContext<StoryHub> hubContext,
    IAuthorizationService authorizationService,
    ApplicationDbContext context,
    INotificationUrlGenerator urlGenerator) : IStoryService
{
    public async Task<Result<MediaStoryDto>> CreateMediaStoryAsync(CreateMediaStoryCommand command)
    {
        var validator = new CreateMediaStoryCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var subFolder = command.MediaType == MediaType.Image ? "Images" : "Videos";

        var (uploaded, fileName) = await fileService.UploadFileAsync(command.Media, $"Stories//{subFolder}");

        if (!uploaded) return Result<MediaStoryDto>.Failure(HttpStatusCode.BadRequest, DomainErrors.FileUploadFailed);

        var mediaStory = new MediaStory()
        {
            Id = Guid.NewGuid(),
            MediaType = command.MediaType,
            UserId = currentUser.Id,
            Caption = command.Caption,
            StoryPrivacy = command.StoryPrivacy,
            MediaUrl = fileName
        };

        unitOfWork.Repository<Story>()?.Create(mediaStory);

        if (command is { StoryPrivacy: StoryPrivacy.Custom, AllowedViewerIds.Count: > 0 })
        {
            foreach (var viewerId in command.AllowedViewerIds.Distinct())
            {
                var isFriend = await unitOfWork.FriendshipRepository.AreFriendsAsync(viewerId, currentUser.Id);

                if (!isFriend)
                    return Result<MediaStoryDto>.Failure(
                        HttpStatusCode.BadRequest,
                        string.Format(DomainErrors.Friendship.NotFriend, viewerId));
                unitOfWork.StoryViewRepository.Create(
                    new StoryView()
                    {
                        Id = Guid.NewGuid(),
                        IsViewed = false,
                        StoryId = mediaStory.Id,
                        ViewerId = viewerId
                    });
            }
        }

        await unitOfWork.SaveChangesAsync();

        var friends = await unitOfWork.FriendshipRepository.GetFriendsOfUserAsync(currentUser.Id);

        if (friends.Count <= 0) return Result<MediaStoryDto>.Success(mapper.Map<MediaStoryDto>(mediaStory));

        switch (mediaStory.StoryPrivacy)
        {
            case StoryPrivacy.Public or StoryPrivacy.Friends:
                {
                    foreach (var friend in friends)
                        await hubContext.Clients.User(friend.Id).SendAsync("NewStoryCreated", new { StoryId = mediaStory.Id, UserId = currentUser.Id });
                    break;
                }
            case StoryPrivacy.Custom:
                var allowedUsersIds = await unitOfWork.StoryViewRepository.GetAllowedUsersAsync(mediaStory.Id);
                foreach (var allowedUserId in allowedUsersIds)
                    await hubContext.Clients.User(allowedUserId)
                        .SendAsync("NewStoryCreated", new
                        {
                            StoryId = mediaStory.Id,
                            UserId = currentUser.Id,
                            currentUser.FullName,
                            ProfilePicture = currentUser.ProfilePictureUrl,
                            StoryType = "media",
                            mediaStory.CreatedAt,
                            mediaStory.ExpiresAt,
                            mediaStory.Caption,
                            mediaStory.MediaUrl,
                            mediaStory.MediaType,
                        });

                break;
        }

        return Result<MediaStoryDto>.Success(mapper.Map<MediaStoryDto>(mediaStory));
    }

    public async Task<Result<TextStoryDto>> CreateTextStoryAsync(CreateTextStoryCommand command)
    {
        var validator = new CreateTextStoryCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var textStory = new TextStory
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            StoryPrivacy = command.StoryPrivacy,
            UserId = currentUser.Id,
            HashTags = command.HashTags
        };

        unitOfWork.Repository<Story>()?.Create(textStory);

        if (command is { StoryPrivacy: StoryPrivacy.Custom, AllowedViewerIds.Count: > 0 })
        {
            foreach (var viewerId in command.AllowedViewerIds.Distinct())
            {
                var isFriend = await unitOfWork.FriendshipRepository.AreFriendsAsync(viewerId, currentUser.Id);

                if (!isFriend)
                    return Result<TextStoryDto>.Failure(
                        HttpStatusCode.BadRequest,
                        string.Format(DomainErrors.Friendship.NotFriend, viewerId));
                unitOfWork.StoryViewRepository.Create(
                    new StoryView()
                    {
                        Id = Guid.NewGuid(),
                        IsViewed = false,
                        StoryId = textStory.Id,
                        ViewerId = viewerId
                    });
            }
        }

        await unitOfWork.SaveChangesAsync();

        var friends = await unitOfWork.FriendshipRepository.GetFriendsOfUserAsync(currentUser.Id);

        var mappedResult = mapper.Map<TextStoryDto>(textStory);

        if (friends.Count <= 0) return Result<TextStoryDto>.Success(mappedResult);

        switch (textStory.StoryPrivacy)
        {
            case StoryPrivacy.Public or StoryPrivacy.Friends:
                {
                    foreach (var friend in friends)
                        await hubContext.Clients.User(friend.Id).SendAsync("NewStoryCreated",
                            new
                            {
                                StoryId = textStory.Id,
                                UserId = currentUser.Id,
                                currentUser.FullName,
                                ProfilePicture = currentUser.ProfilePictureUrl,
                                StoryType = "text",
                                textStory.CreatedAt,
                                textStory.ExpiresAt,
                                textStory.Content,
                                textStory.HashTags
                            });
                    break;
                }
            case StoryPrivacy.Custom:
                var allowedUsersIds = await unitOfWork.StoryViewRepository.GetAllowedUsersAsync(textStory.Id);
                foreach (var allowedUserId in allowedUsersIds)
                    await hubContext.Clients.User(allowedUserId)
                        .SendAsync("NewStoryCreated", new
                        {
                            StoryId = textStory.Id,
                            UserId = currentUser.Id,
                            currentUser.FullName,
                            ProfilePicture = currentUser.ProfilePictureUrl,
                            StoryType = "text",
                            textStory.CreatedAt,
                            textStory.ExpiresAt,
                            textStory.Content,
                            textStory.HashTags
                        });

                break;
        }

        return Result<TextStoryDto>.Success(mappedResult);
    }

    public async Task<Result<IEnumerable<StoryDto>>> GetActiveFriendStoriesAsync(
        GetActiveFriendStoriesQuery query)
    {
        var friendsIds = await unitOfWork.FriendshipRepository.GetFriendIdsForUserAsync(currentUser.Id);

        if (friendsIds.Count == 0)
            return Result<IEnumerable<StoryDto>>.Success([]);

        var specification = new GetActiveFriendsStoriesSpecification(friendsIds, currentUser.Id);

        var activeStories = await unitOfWork.StoryRepository.GetAllWithSpecificationAsync(specification);

        var mappedResults = mapper.Map<IEnumerable<StoryDto>>(activeStories);

        return Result<IEnumerable<StoryDto>>.Success(mappedResults);
    }

    public async Task<Result<IEnumerable<StoryDto>>> GetMyStoriesAsync()
    {
        var specification = new GetUserActiveStoriesSpecification(currentUser.Id);
        var myActiveStories = await unitOfWork.Repository<Story>()?.GetAllWithSpecificationAsync(specification)!;
        var mappedResults = mapper.Map<IEnumerable<StoryDto>>(myActiveStories);
        return Result<IEnumerable<StoryDto>>.Success(mappedResults);
    }

    public async Task<Result<bool>> DeleteStoryAsync(DeleteStoryCommand command)
    {
        var specification = new GetActiveStorySpecification(command.StoryId, currentUser.Id);

        var activeStory = await unitOfWork.Repository<Story>()?.GetBySpecificationAsync(specification)!;

        var authResult = await authorizationService.AuthorizeAsync(
            user: currentUser.GetUser()!,
            resource: activeStory,
            policyName: StoryPolicies.DeleteStory);

        if (!authResult.Succeeded)
            return Result<bool>.Failure(HttpStatusCode.Forbidden);

        switch (activeStory)
        {
            case null:
                return Result<bool>.Failure(
                    statusCode: HttpStatusCode.NotFound,
                    error: string.Format(DomainErrors.Story.StoryNotFounded, command.StoryId));
            case MediaStory mediaStory:
                {
                    var subFolder = mediaStory.MediaType == MediaType.Image ? "Images" : "Videos";
                    var isDeleted = fileService.DeleteFileFromPath(mediaStory.MediaUrl!, $"Stories\\{subFolder}");

                    if (!isDeleted)
                    {
                        return Result<bool>.Failure(HttpStatusCode.BadRequest, DomainErrors.FailedToDeleteMedia);
                    }

                    break;
                }
        }

        unitOfWork.Repository<Story>()?.Delete(activeStory);

        await unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true,
            string.Format(AppConstants.Story.StoryDeleteCompleted, command.StoryId));
    }

    public async Task<Result<bool>> MarkStoryAsViewedAsync(MarkStoryAsViewedCommand command)
    {
        var specification = new GetActiveStorySpecification(command.StoryId);

        var activeStory = await unitOfWork.Repository<Story>()?.GetBySpecificationAsync(specification)!;

        if (activeStory is null)
            return Result<bool>.Failure(
                statusCode: HttpStatusCode.NotFound,
                error: string.Format(DomainErrors.Story.StoryNotFounded, command.StoryId));

        // Check is the story viewed already
        var isViewed = await unitOfWork.StoryViewRepository.IsStoryViewedAsync(activeStory.Id, currentUser.Id);

        if (isViewed)
            return Result<bool>.Failure(
                HttpStatusCode.Conflict,
                string.Format(DomainErrors.Story.StoryViewedYet, activeStory.Id));

        var authResult =
            await authorizationService.AuthorizeAsync(
                user: currentUser.GetUser()!,
                resource: activeStory,
                policyName: StoryPolicies.ViewStory);

        if (!authResult.Succeeded)
            return Result<bool>.Failure(statusCode: HttpStatusCode.Forbidden);

        // Check if the user set as viewer to the story

        if (!await unitOfWork.StoryViewRepository.IsSetAsStoryViewerAsync(activeStory.Id, currentUser.Id))
        {
            var storyView = new StoryView()
            {
                Id = Guid.NewGuid(),
                IsViewed = true,
                StoryId = activeStory.Id,
                ViewedAt = DateTimeOffset.Now,
                ViewerId = currentUser.Id
            };

            unitOfWork.Repository<StoryView>()?.Create(storyView);
        }
        else
        {
            // The User is set as viewer
            var storyViewSpecification = new GetStoryViewForViewerSpecification(currentUser.Id);
            var view = await unitOfWork.StoryViewRepository.GetBySpecificationAsync(storyViewSpecification);

            if (view is null)
                return Result<bool>.Failure(HttpStatusCode.NotFound, DomainErrors.StoryView.ViewNotFound); // TODO

            view.IsViewed = true;
            view.ViewedAt = DateTimeOffset.Now;

            unitOfWork.StoryViewRepository.Update(view);
        }

        await unitOfWork.SaveChangesAsync();

        // Notify The Creator Of The Story With The View

        var totalViewsCount = await unitOfWork.StoryViewRepository.GetTotalViewsForStoryAsync(activeStory.Id);
        var storyViewers = await unitOfWork.StoryViewRepository.GetStoryViewersAsync(activeStory.Id);

        await hubContext.Clients.User(activeStory.UserId).SendAsync(
            "NewStoryView",
            new
            {
                StoryId = activeStory.Id,
                ViewerId = currentUser.Id,
                TotalViews = totalViewsCount,
                Viewers = storyViewers
            });

        return Result<bool>.Success(true);

    }

    public async Task<Result<StoryDto>> GetStoryAsync(GetStoryByIdQuery query)
    {
        var specification = new GetActiveStorySpecification(query.StoryId, currentUser.Id);

        var activeStory = await unitOfWork.Repository<Story>()?.GetBySpecificationAsync(specification)!;

        if (activeStory is null)
            return Result<StoryDto>.Failure(
            statusCode: HttpStatusCode.NotFound,
                error: string.Format(DomainErrors.Story.StoryNotFounded, query.StoryId));

        var mappedResult = mapper.Map<StoryDto>(activeStory);

        return Result<StoryDto>.Success(mappedResult);
    }

    public async Task<Result<bool>> HasUnseenStoriesAsync(HasUnseenStoriesQuery query)
    {
        var validator = new HasUnseenStoriesQueryValidator();
        await validator.ValidateAndThrowAsync(query);

        var isFriend = await unitOfWork.FriendshipRepository.AreFriendsAsync(query.FriendId, currentUser.Id);

        if (!isFriend)
            return Result<bool>.Failure(
                statusCode: HttpStatusCode.BadRequest,
                error: string.Format(DomainErrors.Friendship.NotFriend, query.FriendId)
                );

        var hasUnseenStories = await unitOfWork.StoryRepository.HasUnseenStoriesAsync(
            currentUserId: currentUser.Id,
            friendId: query.FriendId);

        return Result<bool>.Success(hasUnseenStories);
    }

    public async Task<Result<UserWithStoriesDto?>> GetActiveUserStoriesAsync(GetUserStoriesQuery query)
    {
        var isFriend = await unitOfWork.FriendshipRepository.AreFriendsAsync(
            query.FriendId.ToString(), currentUser.Id);

        if (!isFriend)
            return Result<UserWithStoriesDto?>.Failure(HttpStatusCode.Forbidden);

        var activeUserStories = await unitOfWork.StoryRepository.GetActiveUserStoriesAsync(
            currentUserId: currentUser.Id,
            friendId: query.FriendId);

        return Result<UserWithStoriesDto?>.Success(activeUserStories);
    }

    public async Task<Result<bool>> IsStoryViewedAsync(IsStoryViewedQuery query)
    {
        var existedStory = await unitOfWork.StoryRepository.GetByIdAsync(query.StoryId);

        if (existedStory is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound,
                string.Format(DomainErrors.Story.StoryNotFounded, query.StoryId));

        var isViewed = await unitOfWork.StoryViewRepository.IsStoryViewedAsync(query.StoryId, currentUser.Id);

        return Result<bool>.Success(isViewed);
    }

    public async Task<Result<StoryViewsResponseDto?>> GetStoryViewsAsync(GetStoryViewersQuery query)
    {
        var existedStory = await unitOfWork.StoryRepository.GetByIdAsync(query.StoryId);

        if (existedStory is null)
            return Result<StoryViewsResponseDto?>.Failure(
                statusCode: HttpStatusCode.NotFound,
                error: string.Format(DomainErrors.Story.StoryNotFounded, query.StoryId));

        var views = await unitOfWork.StoryRepository.GetStoryViewsAsync(existedStory.Id, currentUser.Id);

        return Result<StoryViewsResponseDto?>.Success(views);
    }

    public async Task<Result<List<StoryViewedDto>>> GetStoriesViewedByMeAsync()
        => Result<List<StoryViewedDto>>.Success(await unitOfWork.StoryRepository.GetStoriesViewedByMeAsync(currentUser.Id));

    public async Task<Result<PagedResult<StoryViewsResponseDto>>> GetStoriesByParamsAsync(GetStoriesByParamsQuery query)
        => Result<PagedResult<StoryViewsResponseDto>>.Success(await unitOfWork.StoryRepository.GetStoriesWithParamsForMeAsync(currentUser.Id, query.StoryQueryParameters));

    public async Task<Result<PagedResult<StoryViewsResponseDto>>> GetExpiredStoriesAsync(StoryQueryParameters? parameters)
        => Result<PagedResult<StoryViewsResponseDto>>.Success(await unitOfWork.StoryRepository.GetExpiredStoriesAsync(currentUser.Id, parameters));

    public async Task<Result<PagedResult<StoryViewsResponseDto>>> GetStoryArchiveAsync(StoryQueryParameters? parameters)
        => Result<PagedResult<StoryViewsResponseDto>>.Success(await unitOfWork.StoryRepository.GetStoryArchiveAsync(currentUser.Id, parameters));

    public async Task<Result<bool>> ReactToStoryAsync(AddStoryReactionCommand command)
    {
        var specification = new GetActiveStorySpecification(command.StoryId);

        var activeStory = await unitOfWork.Repository<Story>()?.GetBySpecificationAsync(specification)!;

        if (activeStory is null)
            return Result<bool>.Failure(
                statusCode: HttpStatusCode.NotFound,
                error: string.Format(DomainErrors.Story.StoryNotFounded, command.StoryId));

        var authResult = await authorizationService.AuthorizeAsync(
            user: currentUser.GetUser()!,
            resource: activeStory,
            policyName: StoryPolicies.React);


        if (!authResult.Succeeded) return Result<bool>.Failure(HttpStatusCode.Forbidden);

        var storyReaction = new StoryReaction
        {
            Id = Guid.NewGuid(),
            ReactedById = currentUser.Id,
            StoryId = command.StoryId,
            ReactionType = command.ReactionType
        };

        unitOfWork.Repository<StoryReaction>()?.Create(storyReaction);


        var notification = new StoryNotification
        {
            Id = Guid.NewGuid(),
            ActorId = currentUser.Id,
            RecipientId = activeStory.UserId,
            Status = NotificationStatus.UnRead,
            Type = NotificationType.NewStoryReaction,
            StoryId = activeStory.Id
        };

        notification.ActionUrl = urlGenerator.GenerateUrl(notification);
        notification.Message = notification.GenerateNotificationText(currentUser.FullName);

        unitOfWork.Repository<StoryNotification>()?.Create(notification);

        await unitOfWork.SaveChangesAsync();

        var storyStatistics = await unitOfWork.StoryRepository.GetStoryViewsAsync(activeStory.Id, activeStory.UserId);

        await hubContext.Clients.User(activeStory.UserId).SendAsync(
            "NewReaction",
            notification.GenerateNotificationText(currentUser.FullName),
            storyReaction.ReactionType.ToString(),
            storyStatistics,
            notification);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ChangeStoryPrivacy(ChangeStoryPrivacyCommand command)
    {
        await new ChangeStoryPrivacyCommandValidator().ValidateAndThrowAsync(command);

        var specification = new GetActiveStorySpecification(command.StoryId, currentUser.Id);

        var activeStory = await unitOfWork.Repository<Story>()?.GetBySpecificationAsync(specification)!;

        if (activeStory is null)
            return Result<bool>.Failure(
                HttpStatusCode.NotFound,
                string.Format(DomainErrors.Story.StoryNotFounded, command.StoryId));

        var authResult = await authorizationService.AuthorizeAsync(
            user: currentUser.GetUser()!,
            resource: activeStory,
            policyName: StoryPolicies.EditStory);

        if (!authResult.Succeeded)
            return Result<bool>.Failure(HttpStatusCode.Forbidden);

        // check is the story privacy is set to custom peoples

        if (activeStory.StoryPrivacy == StoryPrivacy.Custom)
        {
            // Get the story viewers that they are not viewed the story
            var unviewedViewers = activeStory.StoryViewers.Where(s => !s.IsViewed).ToList();

            if (command.Privacy is StoryPrivacy.Private or StoryPrivacy.Friends or StoryPrivacy.Public)
                foreach (var unviewedViewer in unviewedViewers)
                    activeStory.StoryViewers.Remove(unviewedViewer);
        }

        if (command is { Privacy: StoryPrivacy.Custom, AllowedViewerIds.Count: > 0 })
        {
            foreach (var viewerId in command.AllowedViewerIds.Distinct())
            {
                var isFriend = await unitOfWork.FriendshipRepository.AreFriendsAsync(viewerId, currentUser.Id);

                if (!isFriend)
                    return Result<bool>.Failure(
                        HttpStatusCode.BadRequest,
                        string.Format(DomainErrors.Friendship.NotFriend, viewerId));

                unitOfWork.StoryViewRepository.Create(
                    new StoryView()
                    {
                        Id = Guid.NewGuid(),
                        IsViewed = false,
                        StoryId = command.StoryId,
                        ViewerId = viewerId
                    });
            }
        }

        activeStory.StoryPrivacy = command.Privacy;

        unitOfWork.Repository<Story>()?.Update(activeStory);

        // Notify the caller (Me)

        var notification = new StoryNotification
        {
            Id = Guid.NewGuid(),
            ActorId = currentUser.Id,
            RecipientId = currentUser.Id,
            Privacy = command.Privacy.ToString(),
            Status = NotificationStatus.UnRead,
            StoryId = command.StoryId,
            Type = NotificationType.PrivacyChanged
        };

        notification.Message = notification.GenerateNotificationText(currentUser.FullName);

        unitOfWork.Repository<StoryNotification>()?.Create(notification);

        await unitOfWork.SaveChangesAsync();

        await hubContext.Clients.User(currentUser.Id).SendAsync("StoryPrivacyChanged", notification);

        return Result<bool>.Success(true, AppConstants.Story.StoryPrivacyUpdated);
    }

    public async Task<Result<bool>> CommentToStoryAsync(AddStoryCommentCommand command)
    {
        var specification = new GetActiveStorySpecification(command.StoryId);

        var activeStory = await unitOfWork.Repository<Story>()?.GetBySpecificationAsync(specification)!;

        if (activeStory is null)
            return Result<bool>.Failure(
                HttpStatusCode.NotFound,
                string.Format(DomainErrors.Story.StoryNotFounded, command.StoryId));

        var authResult = await authorizationService.AuthorizeAsync(
            user: currentUser.GetUser()!,
            resource: activeStory,
            policyName: StoryPolicies.Comment);

        if (!authResult.Succeeded)
            return Result<bool>.Failure(HttpStatusCode.Forbidden);

        var comment = new StoryComment()
        {
            Id = Guid.NewGuid(),
            CommentedById = currentUser.Id,
            StoryId = activeStory.Id,
            Comment = command.Comment
        };

        activeStory.StoryComments.Add(comment);

        var notification = new StoryNotification
        {
            Id = Guid.NewGuid(),
            ActorId = currentUser.Id,
            RecipientId = activeStory.UserId,
            Status = NotificationStatus.UnRead,
            StoryId = activeStory.Id,
            Type = NotificationType.NewStoryComment
        };

        notification.Message = notification.GenerateNotificationText(currentUser.FullName);

        unitOfWork.Repository<StoryNotification>()?.Create(notification);

        await unitOfWork.SaveChangesAsync();

        // Notify the Owner with the comment

        await hubContext.Clients.User(activeStory.UserId)
            .SendAsync("NewStoryComment",
                notification.GenerateNotificationText(currentUser.FullName),
                notification);

        return Result<bool>.Success(true);
    }
}
