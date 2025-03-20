using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.VerifyDevice;
public sealed class VerifyDeviceCommandHandler(IAuthService service) :
    IRequestHandler<VerifyDeviceCommand, Result<SignInResponseDto>>
{
    public async Task<Result<SignInResponseDto>> Handle(
        VerifyDeviceCommand request, CancellationToken cancellationToken)
        => await service.VerifyDeviceAsync(request);
}
