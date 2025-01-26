using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.VerifyMfaLogin;
public sealed class VerifyMfaLoginCommandHandler(IAuthService authService)
    : IRequestHandler<VerifyMfaLoginCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(
        VerifyMfaLoginCommand request,
        CancellationToken cancellationToken) => await authService.VerifyMfaLoginAsync(request);
}
