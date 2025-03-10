using FluentValidation;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Helpers;

namespace Sociam.Application.Features.Users.Commands.UpdateCover;

public sealed class UpdateCoverCommandValidator : AbstractValidator<UpdateCoverCommand>
{
    public UpdateCoverCommandValidator()
    {
        RuleFor(c => c.Cover)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .Must(avatar => avatar.Length > 0).WithMessage("{PropertyName} can not be empty.")
            .Must(IsValidCover).WithMessage("Not a valid cover format.");
    }

    private static bool IsValidCover(IFormFile avatar)
    {
        var avatarExtension = Path.GetExtension(avatar.FileName).ToLower();
        return FileFormats.AllowedImageFormats.Contains(avatarExtension);
    }
}