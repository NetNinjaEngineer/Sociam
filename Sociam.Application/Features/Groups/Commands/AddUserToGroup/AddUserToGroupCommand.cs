using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Groups.Commands.AddUserToGroup;
public sealed class AddUserToGroupCommand : IRequest<Result<bool>>
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
}