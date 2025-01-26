using MediatR;
using Microsoft.AspNetCore.Http;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Auth;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Auth.Commands.Register;
public sealed class RegisterCommand : IRequest<Result<RegisterResponseDto>>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public IFormFile? Picture { get; set; }
    public string ConfirmPassword { get; set; } = string.Empty;
}