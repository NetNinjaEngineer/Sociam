using FluentValidation;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.CreateTextStory;

public sealed class CreateTextStoryCommandValidator : AbstractValidator<CreateTextStoryCommand>
{
    public CreateTextStoryCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotNull().WithMessage("Content can not be null")
            .NotEmpty().WithMessage("Content can not be empty");

        RuleFor(x => x.StoryPrivacy)
            .IsInEnum().WithMessage("Invalid Privacy Type.")
            .Must((command, privacy) => IsStoryViewersRequired(privacy, command.AllowedViewerIds))
            .WithMessage("You must assign the viewers of the story as you selected the custom story privacy option !!!");
    }

    private static bool IsStoryViewersRequired(StoryPrivacy privacy, List<string>? commandAllowedViewerIds)
        => privacy == StoryPrivacy.Custom && commandAllowedViewerIds?.Count > 0;
}