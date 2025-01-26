using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Queries.GetUserRoles;
public sealed class GetUserRolesQueryHandler(
    IRoleService roleService) : IRequestHandler<GetUserRolesQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(
        GetUserRolesQuery request,
        CancellationToken cancellationToken)
        => await roleService.GetUserRoles(request.UserId);
}
