using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.ValidateToken;
public sealed class ValidateTokenCommandHandler(
    IAuthService authService) : IRequestHandler<ValidateTokenCommand, Result<ValidateTokenResponseDto>>
{
    public async Task<Result<ValidateTokenResponseDto>> Handle(
        ValidateTokenCommand request,
        CancellationToken cancellationToken)
        => await authService.ValidateTokenAsync(request);
}
