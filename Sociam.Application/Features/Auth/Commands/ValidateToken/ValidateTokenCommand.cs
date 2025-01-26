using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;

namespace Sociam.Application.Features.Auth.Commands.ValidateToken;
public sealed class ValidateTokenCommand : IRequest<Result<ValidateTokenResponseDto>>
{
    public string JwtToken { get; set; } = null!;
}
