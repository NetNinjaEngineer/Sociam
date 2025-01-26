using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;

namespace Sociam.Application.Features.Auth.Commands.Verify2FaCode;
public sealed class Verify2FaCodeCommand : IRequest<Result<SignInResponseDto>>
{
    public string Code { get; set; } = null!;
}