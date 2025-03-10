using FluentValidation;

namespace Sociam.Application.Features.Users.Commands.VerifyChangeEmail;

public sealed class VerifyChangeEmailCommandValidator : AbstractValidator<VerifyChangeEmailCommand>
{
    public VerifyChangeEmailCommandValidator()
    {
        RuleFor(c => c.Code)
            .NotNull().WithMessage("Code is not null.")
            .NotEmpty().WithMessage("Code is required.")
            .Length(6).WithMessage("Code must be exactly 6 characters long.");
    }
}