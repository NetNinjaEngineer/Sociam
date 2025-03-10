using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Users.Commands.VerifyChangeEmail;
public sealed class VerifyChangeEmailCommand : IRequest<Result<string>>
{
    public string Code { get; set; } = null!;
}