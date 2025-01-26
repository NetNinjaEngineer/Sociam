using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Application.Features.Auth.Commands.ConfirmEmail;
using Sociam.Application.Features.Auth.Commands.ConfirmEnable2FaCommand;
using Sociam.Application.Features.Auth.Commands.ConfirmForgotPasswordCode;
using Sociam.Application.Features.Auth.Commands.Disable2Fa;
using Sociam.Application.Features.Auth.Commands.Enable2Fa;
using Sociam.Application.Features.Auth.Commands.EnableMfa;
using Sociam.Application.Features.Auth.Commands.ForgetPassword;
using Sociam.Application.Features.Auth.Commands.LoginUser;
using Sociam.Application.Features.Auth.Commands.RefreshToken;
using Sociam.Application.Features.Auth.Commands.Register;
using Sociam.Application.Features.Auth.Commands.RevokeToken;
using Sociam.Application.Features.Auth.Commands.SendConfirmEmailCode;
using Sociam.Application.Features.Auth.Commands.SignInGoogle;
using Sociam.Application.Features.Auth.Commands.ValidateToken;
using Sociam.Application.Features.Auth.Commands.Verify2FaCode;
using Sociam.Application.Features.Auth.Commands.VerifyMfa;
using Sociam.Application.Features.Auth.Commands.VerifyMfaLogin;
using Sociam.Application.Features.Auth.Queries.GetAccessToken;

namespace Sociam.Application.Interfaces.Services;
public interface IAuthService
{
    Task<Result<RegisterResponseDto>> RegisterAsync(RegisterCommand command);
    Task<Result<GoogleUserProfile?>> VerifyAndGetUserProfileAsync(SignInGoogleCommand command);
    Task<Result<SignInResponseDto>> LoginAsync(LoginUserCommand command);
    Task<Result<SignInResponseDto>> RefreshTokenAsync(RefreshTokenCommand command);
    Task<Result<bool>> RevokeTokenAsync(RevokeTokenCommand command);
    Task<Result<string>> ForgotPasswordAsync(ForgetPasswordCommand command);
    Task<Result<string>> ConfirmForgotPasswordCodeAsync(ConfirmForgotPasswordCodeCommand command);
    Task<Result<string>> Enable2FaAsync(Enable2FaCommand command);
    Task<Result<string>> ConfirmEnable2FaAsync(ConfirmEnable2FaCommand command);
    Task<Result<SignInResponseDto>> Verify2FaCodeAsync(Verify2FaCodeCommand command);
    Task<Result<string>> Disable2FaAsync(Disable2FaCommand command);
    Task<Result<SendCodeConfirmEmailResponseDto>> SendConfirmEmailCodeAsync(SendConfirmEmailCodeCommand command);
    Task<Result<string>> ConfirmEmailAsync(ConfirmEmailCommand command);
    Task LogoutAsync();
    Task<Result<ValidateTokenResponseDto>> ValidateTokenAsync(ValidateTokenCommand command);
    Task<Result<string>> EnableMfaAsync(EnableMfaCommand command);
    Task<Result<bool>> VerifyMfaAndConfirmAsync(VerifyMfaCommand command);
    Task<Result<SignInResponseDto>> VerifyMfaLoginAsync(VerifyMfaLoginCommand command);

    Task<Result<string>> GetAccessTokenAsync(GetAccessTokenQuery query);
}
