using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Queries.GetRoleClaims;
public sealed class GetRoleClaimsQueryHandler(
    IRoleService roleService) : IRequestHandler<GetRoleClaimsQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(GetRoleClaimsQuery request,
        CancellationToken cancellationToken)
        => await roleService.GetRoleClaims(request.RoleName);
}
