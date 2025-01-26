using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories;
public sealed class PrivateConversationRepository(ApplicationDbContext context) :
    GenericRepository<Conversation>(context), IPrivateConversationRepository
{
    public async Task<PrivateConversation?> GetPrivateConversationBetweenAsync(string senderId, string receiverId)
    {
        return await context.PrivateConversations
            .AsNoTracking()
            .Include(conversation => conversation.SenderUser)
            .Include(conversation => conversation.ReceiverUser)
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Attachments)
            .FirstOrDefaultAsync(conversation =>
                conversation.SenderUserId == senderId && conversation.ReceiverUserId == receiverId);
    }

    public async Task<PrivateConversation?> GetPrivateUserConversationsAsync(string userId)
    {
        var userConversation = await context.PrivateConversations
            .Include(conversation => conversation.SenderUser)
            .Include(conversation => conversation.ReceiverUser)
            .Include(conversation => conversation
                .Messages.OrderByDescending(message => message.CreatedAt))
            .ThenInclude(message => message.Attachments)
            .FirstOrDefaultAsync(conversation =>
                conversation.SenderUserId == userId ||
                conversation.ReceiverUserId == userId);

        return userConversation;
    }
}
