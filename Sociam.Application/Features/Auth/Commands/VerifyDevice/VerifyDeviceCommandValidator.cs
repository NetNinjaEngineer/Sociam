using FluentValidation;

namespace Sociam.Application.Features.Auth.Commands.VerifyDevice;

public sealed class VerifyDeviceCommandValidator : AbstractValidator<VerifyDeviceCommand>
{
    public VerifyDeviceCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(c => c.VerificationCode)
            .NotEmpty().WithMessage("VerificationCode is required.")
            .Length(6).WithMessage("VerificationCode should be 6 numbers.");
    }
}