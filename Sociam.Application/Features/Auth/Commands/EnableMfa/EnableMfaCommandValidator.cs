using FluentValidation;

namespace Sociam.Application.Features.Auth.Commands.EnableMfa;
public sealed class EnableMfaCommandValidator : AbstractValidator<EnableMfaCommand>
{
    public EnableMfaCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Not in email format.");
    }
}
