using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Users.Commands.ChangeAccountPassword;
public sealed class ChangeAccountPasswordCommand : IRequest<Result<bool>>
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}