﻿using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Auth.Commands.ConfirmForgotPasswordCode;
public class ConfirmForgotPasswordCodeCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}