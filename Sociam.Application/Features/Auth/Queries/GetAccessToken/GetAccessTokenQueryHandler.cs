using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Auth.Queries.GetAccessToken;
public sealed class GetAccessTokenQueryHandler(IAuthService authService) : IRequestHandler<GetAccessTokenQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
        => await authService.GetAccessTokenAsync(request);
}
