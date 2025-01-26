using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Queries.GetRoleClaims;
public sealed class GetRoleClaimsQuery : IRequest<Result<IEnumerable<string>>>
{
    public string RoleName { get; set; } = string.Empty;
}
