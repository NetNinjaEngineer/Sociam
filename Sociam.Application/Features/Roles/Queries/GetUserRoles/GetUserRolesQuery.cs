using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Queries.GetUserRoles;
public sealed class GetUserRolesQuery : IRequest<Result<IEnumerable<string>>>
{
    public string UserId { get; set; } = string.Empty;
}
