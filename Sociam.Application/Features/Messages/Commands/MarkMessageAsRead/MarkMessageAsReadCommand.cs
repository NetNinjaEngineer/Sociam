using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Messages.Commands.MarkMessageAsRead;
public sealed class MarkMessageAsReadCommand : IRequest<Result<bool>>
{
    public Guid MessageId { get; set; }
}
