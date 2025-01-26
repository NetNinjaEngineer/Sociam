using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.RevokeToken;
public sealed class RevokeTokenCommandHandler(IAuthService authService) : IRequestHandler<RevokeTokenCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        => await authService.RevokeTokenAsync(request);
}
