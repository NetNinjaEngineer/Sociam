using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;

namespace Sociam.Application.Features.Auth.Commands.SignInGoogle;
public sealed class SignInGoogleCommand : IRequest<Result<GoogleUserProfile?>>
{
    public string IdToken { get; set; } = null!;
}
