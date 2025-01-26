using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Queries.GetAllRoles;
public sealed class GetAllRolesQuery : IRequest<Result<IEnumerable<string>>>
{
}
