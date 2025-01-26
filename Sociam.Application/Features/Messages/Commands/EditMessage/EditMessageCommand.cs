using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Messages.Commands.EditMessage;
public sealed class EditMessageCommand : IRequest<Result<bool>>
{
    public Guid MessageId { get; set; }
    public string NewContent { get; set; } = null!;
}
