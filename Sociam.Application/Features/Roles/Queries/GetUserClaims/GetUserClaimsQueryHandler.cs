using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Roles.Queries.GetUserClaims;

public sealed class GetUserClaimsQueryHandler(
    IRoleService roleService) : IRequestHandler<GetUserClaimsQuery, Result<IEnumerable<string>>>
{
    public async Task<Result<IEnumerable<string>>> Handle(
        GetUserClaimsQuery request,
        CancellationToken cancellationToken)
        => await roleService.GetUserClaims(request.UserId);
}