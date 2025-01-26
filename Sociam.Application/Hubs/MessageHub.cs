using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Hubs.Interfaces;

namespace Sociam.Application.Hubs;

[Authorize]
public sealed class MessageHub : Hub<IMessageClient>
{
    public async Task SendPrivateMessage(string receiverId, MessageDto message)
        => await Clients.User(receiverId).ReceivePrivateMessage(message);

    public async Task SendReplyToMessage(
        string receiverId, Guid conversationId, Guid messageId, Guid replyId)
        => await Clients.User(receiverId).ReceiveReplyToMessage(conversationId, messageId, replyId);
}
