using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Commands.AddClaimToRole;
public sealed class AddClaimToRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; } = string.Empty;
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
