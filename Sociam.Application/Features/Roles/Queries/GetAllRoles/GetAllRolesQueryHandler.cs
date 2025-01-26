using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Queries.GetAllRoles;
public sealed class GetAllRolesQueryHandler(
    IRoleService roleService) : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(
        GetAllRolesQuery request,
        CancellationToken cancellationToken)
        => await roleService.GetAllRoles();
}
