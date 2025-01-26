using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;

namespace Sociam.Application.Features.Auth.Commands.RefreshToken;
public sealed class RefreshTokenCommand : IRequest<Result<SignInResponseDto>>
{
    public string Token { get; set; } = string.Empty;
}