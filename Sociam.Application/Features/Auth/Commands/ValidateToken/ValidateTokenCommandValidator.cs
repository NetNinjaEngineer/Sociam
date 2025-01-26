﻿using FluentValidation;

namespace Sociam.Application.Features.Auth.Commands.ValidateToken;

public sealed class ValidateTokenCommandValidator : AbstractValidator<ValidateTokenCommand>
{
    public ValidateTokenCommandValidator()
    {
        RuleFor(x => x.JwtToken)
            .NotEmpty().WithMessage("{PropertyName} can not be empty.")
            .Must(BeValidJwtFormat).WithMessage("Token must be in valid JWT format");
    }

    private static bool BeValidJwtFormat(string token) => token.Split(".").Length == 3;
}
