using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Groups.Commands.JoinGroup;

public sealed class JoinGroupCommand : IRequest<Result<string>>
{
    public Guid GroupId { get; set; }
}
