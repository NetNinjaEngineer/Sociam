using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Queries.IsUsernameAvailable;
public sealed class IsUsernameAvailableQueryHandler(IUserService service) :
    IRequestHandler<IsUsernameAvailableQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        IsUsernameAvailableQuery request, CancellationToken cancellationToken)
        => await service.IsUsernameAvailableAsync(request);
}
