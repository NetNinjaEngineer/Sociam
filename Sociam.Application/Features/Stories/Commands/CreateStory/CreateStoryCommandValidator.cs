using FluentValidation;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Helpers;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.CreateStory;
public sealed class CreateStoryCommandValidator : AbstractValidator<CreateStoryCommand>
{
    public CreateStoryCommandValidator()
    {
        RuleFor(x => x.Media)
            .NotNull().WithMessage("Media is required.")
            .Must(media => media.Length > 0).WithMessage("Media file cannot be empty.")
            .Must((command, media) => IsValidMediaType(media, command.MediaType)).WithMessage("Media file format does not match the specified Media Type.");

        RuleFor(x => x.MediaType)
            .IsInEnum().WithMessage("Invalid Media Type.");
    }

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
