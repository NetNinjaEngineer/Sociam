using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Users.Commands.ChangeAccountEmail;
public sealed class ChangeAccountEmailCommand : IRequest<Result<bool>>
{
    public string NewEmail { get; set; } = null!;
    public string OldEmailPassword { get; set; } = null!;
}