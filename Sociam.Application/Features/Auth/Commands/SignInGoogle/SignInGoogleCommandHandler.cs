using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Commands.SignInGoogle;
public sealed class SignInGoogleCommandHandler(IAuthService authService) : IRequestHandler<SignInGoogleCommand, Result<GoogleUserProfile?>>
{
    public async Task<Result<GoogleUserProfile?>> Handle(SignInGoogleCommand request, CancellationToken cancellationToken)
    {
        return await authService.VerifyAndGetUserProfileAsync(request);
    }
}
