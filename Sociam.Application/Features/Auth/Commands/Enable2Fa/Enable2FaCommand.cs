using MediatR;
using Sociam.Application.Bases;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Auth.Commands.Enable2Fa;
public sealed class Enable2FaCommand : IRequest<Result<string>>
{
    public TokenProvider TokenProvider { get; set; }
    public string Email { get; set; } = null!;
}