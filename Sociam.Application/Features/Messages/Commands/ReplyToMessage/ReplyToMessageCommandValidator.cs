using FluentValidation;

namespace Sociam.Application.Features.Messages.Commands.ReplyToMessage;

public sealed class ReplyToMessageCommandValidator : AbstractValidator<ReplyToMessageCommand>
{
    public ReplyToMessageCommandValidator()
    {
        RuleFor(c => c.Content)
            .NotNull().WithMessage("{PropertyName} should not be null.")
            .NotEmpty().WithMessage("{PropertyName} should not be empty value.")
            .MaximumLength(2000).WithMessage("{PropertyName} should not exceed 2000 characters.");

    }
}
