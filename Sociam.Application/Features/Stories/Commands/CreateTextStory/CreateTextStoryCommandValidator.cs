using FluentValidation;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.CreateTextStory;

public sealed class CreateTextStoryCommandValidator : AbstractValidator<CreateTextStoryCommand>
{
    public CreateTextStoryCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotNull().WithMessage("Content cannot be null")
            .NotEmpty().WithMessage("Content cannot be empty");

        RuleFor(x => x.StoryPrivacy)
            .IsInEnum().WithMessage("Invalid Privacy Type.");

        RuleFor(x => x)
            .Must(command => IsStoryViewersValid(command.StoryPrivacy, command.AllowedViewerIds))
            .WithMessage("You must assign viewers when selecting the custom story privacy option!");
    }

    private static bool IsStoryViewersValid(StoryPrivacy privacy, List<string>? allowedViewerIds)
        => privacy != StoryPrivacy.Custom || allowedViewerIds is { Count: > 0 };
}
