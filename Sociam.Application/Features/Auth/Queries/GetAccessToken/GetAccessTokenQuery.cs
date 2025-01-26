using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Auth.Queries.GetAccessToken;
public sealed class GetAccessTokenQuery : IRequest<Result<string>>
{
}
