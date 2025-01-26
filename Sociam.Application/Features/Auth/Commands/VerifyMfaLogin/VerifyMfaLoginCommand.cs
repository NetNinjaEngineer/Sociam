using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;

namespace Sociam.Application.Features.Auth.Commands.VerifyMfaLogin;
public sealed class VerifyMfaLoginCommand : IRequest<Result<SignInResponseDto>>
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}
