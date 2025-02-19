using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Notifications.Commands.MarkAsRead;
public sealed class MarkAsReadCommand : IRequest<Result<bool>>
{
    public Guid NotificationId { get; set; }
}
