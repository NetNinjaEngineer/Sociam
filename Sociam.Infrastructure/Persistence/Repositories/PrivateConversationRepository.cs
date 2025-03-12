using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories;
public sealed class PrivateConversationRepository(ApplicationDbContext context) :
    GenericRepository<Conversation>(context), IPrivateConversationRepository
{
    public async Task<PrivateConversation?> GetPrivateConversationBetweenAsync(string senderId, string receiverId)
    {
        return await context.Conversations.OfType<PrivateConversation>()
            .AsNoTracking()
            .Include(conversation => conversation.SenderUser)
            .Include(conversation => conversation.ReceiverUser)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Attachments)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Reactions)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Mentions)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Replies)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Sender)
            .OrderByDescending(conversation => conversation.CreatedAt)
            .FirstOrDefaultAsync(conversation =>
                conversation.SenderUserId == senderId && conversation.ReceiverUserId == receiverId);
    }

    public async Task<PrivateConversation?> GetPrivateUserConversationsAsync(string userId)
    {
        var userConversation = await context.Conversations.OfType<PrivateConversation>()
            .Include(conversation => conversation.SenderUser)
            .Include(conversation => conversation.ReceiverUser)
            .Include(conversation => conversation
                .Messages.OrderByDescending(message => message.CreatedAt))
            .ThenInclude(message => message.Attachments)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Reactions)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Mentions)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Replies)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Sender)
            .FirstOrDefaultAsync(conversation =>
                conversation.SenderUserId == userId ||
                conversation.ReceiverUserId == userId);

        return userConversation;
    }
}
