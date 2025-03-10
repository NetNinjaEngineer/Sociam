using FluentValidation;

namespace Sociam.Application.Features.Users.Commands.ChangeAccountEmail;

public sealed class ChangeAccountEmailCommandValidator : AbstractValidator<ChangeAccountEmailCommand>
{
    public ChangeAccountEmailCommandValidator()
    {
        RuleFor(x => x.NewEmail)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.OldEmailPassword)
            .NotNull().WithMessage("Password cannot be null.")
            .NotEmpty().WithMessage("Password is required")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$")
            .WithMessage(
                "Password must be at least 8 characters long, include at least one lowercase letter, one uppercase letter, one digit, and one special character (e.g., !@#$%^&*).");
    }
}