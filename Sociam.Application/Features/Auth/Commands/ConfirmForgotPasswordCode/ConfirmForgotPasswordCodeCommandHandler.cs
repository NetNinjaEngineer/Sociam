using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.ConfirmForgotPasswordCode;
public class ConfirmForgotPasswordCodeCommandHandler(IAuthService authService) :
    IRequestHandler<ConfirmForgotPasswordCodeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        ConfirmForgotPasswordCodeCommand request,
        CancellationToken cancellationToken)
        => await authService.ConfirmForgotPasswordCodeAsync(request);
}