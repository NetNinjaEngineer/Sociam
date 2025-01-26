using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.Disable2Fa;
public sealed class Disable2FaCommandHandler(IAuthService authService)
    : IRequestHandler<Disable2FaCommand, Result<string>>
{
    public async Task<Result<string>> Handle(Disable2FaCommand request, CancellationToken cancellationToken)
        => await authService.Disable2FaAsync(request);
}