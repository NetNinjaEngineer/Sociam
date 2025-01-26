using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.Verify2FaCode;
public sealed class Verify2FaCodeCommandHandler(IAuthService authService)
    : IRequestHandler<Verify2FaCodeCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(Verify2FaCodeCommand request,
        CancellationToken cancellationToken)
        => await authService.Verify2FaCodeAsync(request);
}