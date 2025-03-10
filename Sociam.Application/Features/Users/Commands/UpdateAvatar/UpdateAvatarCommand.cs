using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Users.Commands.UpdateAvatar;
public sealed class UpdateAvatarCommand : IRequest<Result<string>>
{
    public IFormFile Avatar { get; set; } = null!;
}