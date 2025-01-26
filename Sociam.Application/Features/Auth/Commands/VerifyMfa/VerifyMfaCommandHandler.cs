using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.VerifyMfa;

public sealed class VerifyMfaCommandHandler(IAuthService authService) : IRequestHandler<VerifyMfaCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(VerifyMfaCommand request, CancellationToken cancellationToken)
        => await authService.VerifyMfaAndConfirmAsync(request);
}
