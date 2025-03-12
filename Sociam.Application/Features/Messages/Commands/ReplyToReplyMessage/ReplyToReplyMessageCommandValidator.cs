using FluentValidation;

namespace Sociam.Application.Features.Messages.Commands.ReplyToReplyMessage;

public class ReplyToReplyMessageCommandValidator : AbstractValidator<ReplyToReplyMessageCommand>
{
    public ReplyToReplyMessageCommandValidator()
    {
        RuleFor(x => x.ParentReplyId)
            .NotEmpty()
            .WithMessage("Parent reply ID is required");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required")
            .MaximumLength(1000)
            .WithMessage("Content cannot exceed 1000 characters");
    }
}