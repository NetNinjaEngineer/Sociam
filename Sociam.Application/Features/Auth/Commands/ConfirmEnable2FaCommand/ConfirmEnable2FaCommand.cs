using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Auth.Commands.ConfirmEnable2FaCommand;
public sealed class ConfirmEnable2FaCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
}
