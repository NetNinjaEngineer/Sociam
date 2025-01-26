using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Roles.Queries.GetUserClaims;

public sealed class GetUserClaimsQuery : IRequest<Result<IEnumerable<string>>>
{
    public string UserId { get; set; } = string.Empty;
}