using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;

namespace Sociam.Application.Features.Auth.Commands.VerifyDevice;
public sealed class VerifyDeviceCommand : IRequest<Result<SignInResponseDto>>
{
    public string Email { get; set; } = null!;
    public string VerificationCode { get; set; } = null!;
}
