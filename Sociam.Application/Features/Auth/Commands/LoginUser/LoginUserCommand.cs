using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;

namespace Sociam.Application.Features.Auth.Commands.LoginUser;
public sealed class LoginUserCommand : IRequest<Result<SignInResponseDto>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
