using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Authorization;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;
using Sociam.Application.Features.Stories.Commands.DeleteStory;
using Sociam.Application.Features.Stories.Commands.MarkAsViewed;
using Sociam.Application.Features.Stories.Queries.GetActiveFriendStories;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
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
    public async Task<Result<StoryDto>> CreateStoryAsync(CreateStoryCommand command)
    {
        var validator = new CreateStoryCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var subFolder = command.MediaType == MediaType.Image ? "Images" : "Videos";

        var (uploaded, fileName) = await fileService.UploadFileAsync(command.Media, $"Stories//{subFolder}");

        if (!uploaded) return Result<StoryDto>.Failure(HttpStatusCode.BadRequest, DomainErrors.FileUploadFailed);

        var mappedStory = mapper.Map<Story>(command);
        mappedStory.MediaUrl = fileName;
        mappedStory.UserId = currentUser.Id;

        unitOfWork.Repository<Story>()?.Create(mappedStory);
        await unitOfWork.SaveChangesAsync();

        var friends = await unitOfWork.FriendshipRepository.GetFriendsOfUserAsync(currentUser.Id);

        if (friends.Count <= 0) return Result<StoryDto>.Success(mapper.Map<StoryDto>(mappedStory));

        foreach (var friend in friends)
            await hubContext.Clients.User(friend.Id).SendAsync("NewStoryCreated", new { StoryId = mappedStory.Id, UserId = currentUser.Id });

        return Result<StoryDto>.Success(mapper.Map<StoryDto>(mappedStory));

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

    public async Task<Result<bool>> DeleteStoryAsync(DeleteStoryCommand command)
    {
        var specification = new GetActiveStorySpecification(command.StoryId, currentUser.Id);

        var activeStory = await unitOfWork.Repository<Story>()?.GetBySpecificationAsync(specification)!;

        if (activeStory is null)
            return Result<bool>.Failure(
                statusCode: HttpStatusCode.NotFound,
                error: string.Format(DomainErrors.Story.StoryNotFounded, command.StoryId));

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
