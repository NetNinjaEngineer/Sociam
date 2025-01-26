using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.SendConfirmEmailCode;
public sealed class SendConfirmEmailCodeCommandHandler(
    IAuthService authService) : IRequestHandler<SendConfirmEmailCodeCommand, Result<SendCodeConfirmEmailResponseDto>>
{
    public async Task<Result<SendCodeConfirmEmailResponseDto>> Handle(
        SendConfirmEmailCodeCommand request,
        CancellationToken cancellationToken)
        => await authService.SendConfirmEmailCodeAsync(request);
}
