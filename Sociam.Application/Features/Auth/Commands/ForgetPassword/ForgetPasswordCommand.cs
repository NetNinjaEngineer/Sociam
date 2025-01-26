using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Auth.Commands.ForgetPassword;
public sealed class ForgetPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
}
