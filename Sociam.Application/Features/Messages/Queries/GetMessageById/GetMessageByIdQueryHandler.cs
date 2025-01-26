using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Queries.GetMessageById;
public sealed class GetMessageByIdQueryHandler(
    IMessageService messageService) : IRequestHandler<GetMessageByIdQuery, Result<MessageDto>>
{
    public async Task<Result<MessageDto>> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        => await messageService.GetMessageByIdAsync(request.MessageId);
}
