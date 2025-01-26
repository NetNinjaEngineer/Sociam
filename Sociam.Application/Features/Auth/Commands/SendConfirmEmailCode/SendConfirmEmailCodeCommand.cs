using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;

namespace Sociam.Application.Features.Auth.Commands.SendConfirmEmailCode;
public sealed class SendConfirmEmailCodeCommand : IRequest<Result<SendCodeConfirmEmailResponseDto>>
{
    public string Email { get; set; } = string.Empty;
}
