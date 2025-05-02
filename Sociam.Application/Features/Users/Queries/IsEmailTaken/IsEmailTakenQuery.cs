using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Users.Queries.IsEmailTaken;
public sealed class IsEmailTakenQuery(string email) : IRequest<Result<bool>>
{
    public string Email { get; private set; } = email;
}
