using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Messages.Commands.DeleteMessage;
public sealed class DeleteMessageCommand : IRequest<Result<bool>>
{
    public Guid MessageId { get; set; }
}
