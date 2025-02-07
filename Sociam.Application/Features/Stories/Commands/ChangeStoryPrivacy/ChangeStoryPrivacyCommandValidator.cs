using FluentValidation;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Stories.Commands.ChangeStoryPrivacy;

public sealed class ChangeStoryPrivacyCommandValidator : AbstractValidator<ChangeStoryPrivacyCommand>
{
    public ChangeStoryPrivacyCommandValidator()
    {
        RuleFor(x => x.Privacy)
            .IsInEnum().WithMessage("Invalid Privacy Type.");

        RuleFor(x => x)
            .Must(command => IsStoryViewersValid(command.Privacy, command.AllowedViewerIds))
            .WithMessage("You must assign viewers when selecting the custom story privacy option!");
    }

    private static bool IsStoryViewersValid(StoryPrivacy privacy, List<string>? allowedViewerIds)
        => privacy != StoryPrivacy.Custom || allowedViewerIds is { Count: > 0 };
}