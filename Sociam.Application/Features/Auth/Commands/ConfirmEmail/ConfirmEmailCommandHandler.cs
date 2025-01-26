using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.ConfirmEmail;
public sealed class ConfirmEmailCommandHandler(
    IAuthService authService) : IRequestHandler<ConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        => await authService.ConfirmEmailAsync(request);
}
