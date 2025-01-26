using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.ConfirmEnable2FaCommand;
public sealed class ConfirmEnable2FaCommandHandler(IAuthService authService)
    : IRequestHandler<ConfirmEnable2FaCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ConfirmEnable2FaCommand request, CancellationToken cancellationToken)
        => await authService.ConfirmEnable2FaAsync(request);
}