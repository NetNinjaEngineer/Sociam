using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Authorization;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateMediaStory;
using Sociam.Application.Features.Stories.Commands.CreateTextStory;
using Sociam.Application.Features.Stories.Commands.DeleteStory;
using Sociam.Application.Features.Stories.Commands.MarkAsViewed;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Specifications;
using System.Net;

namespace Sociam.Services.Services;
public sealed class StoryService(
    IMapper mapper,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork,
    IFileService fileService,
    IHubContext<StoryHub> hubContext,
    IAuthorizationService authorizationService) : IStoryService
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
        await unitOfWork.SaveChangesAsync();

        var friends = await unitOfWork.FriendshipRepository.GetFriendsOfUserAsync(currentUser.Id);

        if (friends.Count <= 0) return Result<MediaStoryDto>.Success(mapper.Map<MediaStoryDto>(mediaStory));

        foreach (var friend in friends)
            await hubContext.Clients.User(friend.Id).SendAsync("NewStoryCreated", new { StoryId = mediaStory.Id, UserId = currentUser.Id });

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
        await unitOfWork.SaveChangesAsync();

        var friends = await unitOfWork.FriendshipRepository.GetFriendsOfUserAsync(currentUser.Id);

        var mappedResult = mapper.Map<TextStoryDto>(textStory);

        if (friends.Count <= 0) return Result<TextStoryDto>.Success(mappedResult);

        foreach (var friend in friends)
            await hubContext.Clients.User(friend.Id).SendAsync("NewStoryCreated", new { StoryId = textStory.Id, UserId = currentUser.Id });

        return Result<TextStoryDto>.Success(mappedResult);
    }

    public async Task<Result<IEnumerable<StoryDto>>> GetActiveFriendStoriesAsync(
        GetActiveFriendStoriesQuery query)
    {
        var friends = await unitOfWork.FriendshipRepository.GetFriendsOfUserAsync(currentUser.Id);

        if (friends.Count == 0)
            return Result<IEnumerable<StoryDto>>.Success([]);

        var specification = new GetActiveFriendsStoriesSpecification(friends);

        var activeStories = await unitOfWork.Repository<Story>()?
            .GetAllWithSpecificationAsync(specification)!;

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
}
