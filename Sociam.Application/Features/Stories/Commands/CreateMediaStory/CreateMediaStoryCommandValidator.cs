using FluentValidation;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Helpers;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.CreateMediaStory;
public sealed class CreateMediaStoryCommandValidator : AbstractValidator<CreateMediaStoryCommand>
{
    public CreateMediaStoryCommandValidator()
    {
        RuleFor(x => x.Media)
            .NotNull().WithMessage("Media is required.")
            .Must(media => media.Length > 0).WithMessage("Media file cannot be empty.")
            .Must((command, media) => IsValidMediaType(media, command.MediaType)).WithMessage("Media file format does not match the specified Media Type.");

        RuleFor(x => x.MediaType)
            .IsInEnum().WithMessage("Invalid Media Type.");


        RuleFor(x => x.StoryPrivacy)
            .IsInEnum().WithMessage("Invalid Privacy Type.")
            .Must((command, privacy) => IsStoryViewersRequired(privacy, command.AllowedViewerIds))
            .WithMessage("You must assign the viewers of the story as you selected the custom story privacy option !!!");

    }

    private static bool IsStoryViewersRequired(StoryPrivacy privacy, List<string>? commandAllowedViewerIds)
        => privacy == StoryPrivacy.Custom && commandAllowedViewerIds?.Count > 0;

    private static bool IsValidMediaType(IFormFile media, MediaType mediaType)
    {
        var fileExtension = Path.GetExtension(media.FileName)?.ToLower();

        return mediaType switch
        {
            MediaType.Image => FileFormats.AllowedImageFormats.Contains(fileExtension!),
            MediaType.Video => FileFormats.AllowedVideoFormats.Contains(fileExtension!),
            _ => false
        };
    }
}
