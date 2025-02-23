﻿using Sociam.Application.DTOs.Messages;

namespace Sociam.Application.Hubs.Interfaces;
public interface IMessageClient
{
    Task ReceivePrivateMessage(MessageDto message);
    Task ReceiveReplyToMessage(Guid conversationId, Guid messageId, Guid replyId);
}
