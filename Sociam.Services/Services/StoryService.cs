using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using System.Net;

namespace Sociam.Services.Services;
public sealed class StoryService(
    IMapper mapper,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork,
    IFileService fileService,
    IHubContext<StoryHub> hubContext) : IStoryService
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
}
