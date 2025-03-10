using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Users;
using Sociam.Domain.Enums;

namespace Sociam.Application.Features.Users.Commands.UpdateUserProfile;
public sealed class UpdateUserProfileCommand : IRequest<Result<ProfileDto>>
{
    public string? UserName { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? Bio { get; set; }
}