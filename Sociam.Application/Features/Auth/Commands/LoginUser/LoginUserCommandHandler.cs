using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.LoginUser;
public sealed class LoginUserCommandHandler(
    IAuthService authService,
    IHttpContextAccessor contextAccessor) : IRequestHandler<LoginUserCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request);

        return response;
    }
}