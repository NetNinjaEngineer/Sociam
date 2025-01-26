using FluentValidation;

namespace Sociam.Application.Features.Auth.Commands.VerifyMfa;

public sealed class VerifyMfaCommandValidator : AbstractValidator<VerifyMfaCommand>
{
    public VerifyMfaCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Not in valid format.");
    }
}