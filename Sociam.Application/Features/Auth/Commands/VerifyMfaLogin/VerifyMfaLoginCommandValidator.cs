using FluentValidation;

namespace Sociam.Application.Features.Auth.Commands.VerifyMfaLogin;
public sealed class VerifyMfaLoginCommandValidator : AbstractValidator<VerifyMfaLoginCommand>
{
    public VerifyMfaLoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Not valid email format.");


        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("Code is required");
    }
}
