﻿using MediatR;
using Sociam.Application.Bases;

namespace Sociam.Application.Features.Messages.Commands.DeleteMessageInConversation;
public sealed class DeleteMessageInConversationCommand : IRequest<Result<bool>>
{
    public required Guid ConversationId { get; set; }
    public required Guid MessageId { get; set; }
}
