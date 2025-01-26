using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Auth.Commands.VerifyMfa;
public sealed class VerifyMfaCommand : IRequest<Result<bool>>
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}