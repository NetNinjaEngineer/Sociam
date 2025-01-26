using MediatR;
using Sociam.Application.Bases;
using Sociam.Application.Interfaces.Services;

namespace Sociam.Application.Features.Messages.Commands.EditMessage;
public sealed class EditMessageCommandHandler(
    IMessageService messageService) : IRequestHandler<EditMessageCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        EditMessageCommand request, CancellationToken cancellationToken)
        => await messageService.EditMessageAsync(request.MessageId, request.NewContent);
}
