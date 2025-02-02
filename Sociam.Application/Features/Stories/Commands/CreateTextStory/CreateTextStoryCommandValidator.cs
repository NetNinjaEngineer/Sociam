using FluentValidation;

namespace Sociam.Application.Features.Stories.Commands.CreateTextStory;

public sealed class CreateTextStoryCommandValidator : AbstractValidator<CreateTextStoryCommand>
{
    public CreateTextStoryCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content can not be empty");

        RuleFor(x => x.StoryPrivacy)
            .IsInEnum().WithMessage("Invalid Privacy Type.");
    }
}