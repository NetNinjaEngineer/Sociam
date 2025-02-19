using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Notifications.Commands.MarkAllAsRead;
public sealed class MarkAllAsReadCommand : IRequest<Result<bool>>
{
}
