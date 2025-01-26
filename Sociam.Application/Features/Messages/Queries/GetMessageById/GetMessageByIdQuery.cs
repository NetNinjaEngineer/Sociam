using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;

namespace Sociam.Application.Features.Messages.Queries.GetMessageById;
public sealed class GetMessageByIdQuery : IRequest<Result<MessageDto>>
{
    public Guid MessageId { get; set; }
}
