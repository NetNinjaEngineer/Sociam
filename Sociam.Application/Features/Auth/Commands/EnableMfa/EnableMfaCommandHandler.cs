using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.EnableMfa;

public sealed class EnableMfaCommandHandler(IAuthService authService) :
    IRequestHandler<EnableMfaCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        EnableMfaCommand request, CancellationToken cancellationToken)
        => await authService.EnableMfaAsync(request);
}