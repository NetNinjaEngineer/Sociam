using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Auth.Commands.EnableMfa;
public sealed class EnableMfaCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = null!;
}