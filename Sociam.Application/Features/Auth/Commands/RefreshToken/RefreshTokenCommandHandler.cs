using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.RefreshToken;
public sealed class RefreshTokenCommandHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        => await authService.RefreshTokenAsync(request);
}