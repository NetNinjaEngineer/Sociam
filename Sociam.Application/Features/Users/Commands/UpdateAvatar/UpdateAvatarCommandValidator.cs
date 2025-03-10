using FluentValidation;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Helpers;

namespace Sociam.Application.Features.Users.Commands.UpdateAvatar;

public sealed class UpdateAvatarCommandValidator : AbstractValidator<UpdateAvatarCommand>
{
    public UpdateAvatarCommandValidator()
    {
        RuleFor(c => c.Avatar)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .Must(avatar => avatar.Length > 0).WithMessage("{PropertyName} can not be empty.")
            .Must(IsValidAvatar).WithMessage("Not a valid avatar format.");
    }

    private static bool IsValidAvatar(IFormFile avatar)
    {
        var avatarExtension = Path.GetExtension(avatar.FileName).ToLower();
        return FileFormats.AllowedImageFormats.Contains(avatarExtension);
    }
}