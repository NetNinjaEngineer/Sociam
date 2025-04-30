using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Users.Queries.IsUsernameAvailable;
public sealed class IsUsernameAvailableQuery : IRequest<Result<bool>>
{
    public string Username { get; set; } = null!;
}
