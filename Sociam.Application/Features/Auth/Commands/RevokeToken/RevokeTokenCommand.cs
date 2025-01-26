using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Auth.Commands.RevokeToken;
public sealed class RevokeTokenCommand : IRequest<Result<bool>>
{
    public string? Token { get; set; }
}