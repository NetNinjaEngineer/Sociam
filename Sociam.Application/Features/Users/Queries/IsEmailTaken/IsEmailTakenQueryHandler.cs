using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Users.Queries.IsEmailTaken;
public sealed class IsEmailTakenQueryHandler(IUserService service) : IRequestHandler<IsEmailTakenQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(IsEmailTakenQuery request, CancellationToken cancellationToken)
        => await service.IsEmailTakenAsync(request);
}
