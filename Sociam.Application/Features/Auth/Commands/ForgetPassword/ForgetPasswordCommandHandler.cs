using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.ForgetPassword;
public sealed class ForgetPasswordCommandHandler(
    IAuthService authService) : IRequestHandler<ForgetPasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        ForgetPasswordCommand request,
        CancellationToken cancellationToken)
        => await authService.ForgotPasswordAsync(request);
}
