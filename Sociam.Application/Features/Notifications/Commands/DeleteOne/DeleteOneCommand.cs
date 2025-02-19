using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Notifications.Commands.DeleteOne;
public sealed class DeleteOneCommand : IRequest<Result<bool>>
{
    public Guid NotificationId { get; set; }
}
