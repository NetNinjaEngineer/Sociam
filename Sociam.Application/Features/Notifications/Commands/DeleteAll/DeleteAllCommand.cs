using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Notifications.Commands.DeleteAll;
public sealed class DeleteAllCommand : IRequest<Result<bool>>
{
}
