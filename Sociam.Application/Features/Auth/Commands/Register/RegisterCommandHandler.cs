using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandHandler(
    IAuthService authService) : IRequestHandler<RegisterCommand, Result<RegisterResponseDto>>
{
    public async Task<Result<RegisterResponseDto>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
        => await authService.RegisterAsync(request);
}