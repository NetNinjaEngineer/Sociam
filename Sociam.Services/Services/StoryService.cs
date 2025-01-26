using AutoMapper;
using FluentValidation;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Stories;
using Sociam.Application.Features.Stories.Commands.CreateStory;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Services.Services;
public sealed class StoryService(
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IFileService fileService) : IStoryService
{
    public async Task<Result<StoryDto>> CreateStoryAsync(CreateStoryCommand command)
    {
        var validator = new CreateStoryCommandValidator();
        await validator.ValidateAndThrowAsync(command);

        var subFolder = command.MediaType == MediaType.Image ? "Images" : "Videos";

        var filePath = Path.Combine("Stories", subFolder);
        var (uploaded, fileName) = await fileService.UploadFileAsync(command.Media, filePath);

        var mappedStory = mapper.Map<Story>(command);
        mappedStory.MediaUrl = fileName;
        mappedStory.UserId = currentUser.Id;
        mappedStory.ExpiresAt = DateTimeOffset.Now.AddHours(24);

        unitOfWork.Repository<Story>()?.Create(mappedStory);
        await unitOfWork.SaveChangesAsync();

        return Result<StoryDto>.Success(mapper.Map<StoryDto>(mappedStory));

    }
}
