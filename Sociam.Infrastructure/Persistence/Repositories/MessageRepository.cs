using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Infrastructure.Persistence.Repositories;
public sealed class MessageRepository(ApplicationDbContext context) :
    GenericRepository<Message>(context), IMessageRepository
{
    public async Task<Conversation?> GetConversationMessagesAsync(Guid conversationId)
    {
        var conversation = await context.Conversations
            .Include(conversation => conversation.Messages)
            .ThenInclude(message => message.Attachments)
            .Include(c => c.Messages).ThenInclude(m => m.Reactions)
            .Include(c => c.Messages).ThenInclude(m => m.Sender)
            .Include(c => c.Messages).ThenInclude(m => m.Replies)
            .Include(c => c.Messages).ThenInclude(m => m.Mentions)
            .FirstOrDefaultAsync(c => c.Id == conversationId);

        if (conversation == null)
            return null;

        switch (conversation)
        {
            case PrivateConversation pc:
                await context.Entry(pc)
                    .Reference(p => p.SenderUser)
                    .LoadAsync();
                await context.Entry(pc)
                    .Reference(p => p.ReceiverUser)
                    .LoadAsync();
                break;

            case GroupConversation gc:
                await context.Entry(gc)
                    .Reference(g => g.Group)
                    .LoadAsync();
                break;
        }

        return conversation;
    }

    public async Task<IEnumerable<Message>> GetMessagesByDateRangeAsync(
        DateOnly startDate,
        DateOnly endDate,
        Guid? conversationId = null)
    {
        var todayDate = DateOnly.FromDateTime(DateTime.Today);

        if (startDate > todayDate)
            startDate = todayDate;

        if (endDate > todayDate)
            endDate = todayDate;

        if (endDate < todayDate)
            endDate = todayDate;

        var messages = await context.Messages
            .AsNoTracking()
            .Include(message => message.Attachments)
            .Include(message => message.Reactions)
            .Include(message => message.Mentions)
            .Include(message => message.Replies)
            .Include(message => message.Conversation)
            .Where(message =>
                DateOnly.FromDateTime(message.CreatedAt.Date) >= startDate &&
                DateOnly.FromDateTime(message.CreatedAt.Date) <= endDate &&
                (!conversationId.HasValue || message.ConversationId == conversationId))
            .ToListAsync();


        return messages;
    }

    public async Task<Conversation?> GetPagedConversationMessagesAsync(
        Guid conversationId, int page, int size)
    {
        if (page < 1) page = 1;
        if (size < 1) size = 10;
        if (size > 50) size = 50;

        var pagedConversationMessages = await context.Conversations
            .Include(
                c => c.Messages
                .OrderByDescending(m => m.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size))
            .ThenInclude(m => m.Attachments)
            .Include(c => c.Messages).ThenInclude(m => m.Reactions)
            .Include(c => c.Messages).ThenInclude(m => m.Sender)
            .Include(c => c.Messages).ThenInclude(m => m.Replies)
            .Include(c => c.Messages).ThenInclude(m => m.Mentions)
            .FirstOrDefaultAsync(c => c.Id == conversationId);

        if (pagedConversationMessages == null)
            return null;

        switch (pagedConversationMessages)
        {
            case PrivateConversation pc:
                await context.Entry(pc)
                    .Reference(p => p.SenderUser)
                    .LoadAsync();
                await context.Entry(pc)
                    .Reference(p => p.ReceiverUser)
                    .LoadAsync();
                break;

            case GroupConversation gc:
                await context.Entry(gc)
                    .Reference(g => g.Group)
                    .LoadAsync();
                break;
        }

        return pagedConversationMessages;
    }

    public async Task<IEnumerable<Message>> GetUnreadMessagesAsync(string userId, Guid? groupId = null)
    {
        var unreadMessages = await context.Messages
            .AsNoTracking()
            .Include(message => message.Reactions)
            .Include(message => message.Mentions)
            .Include(message => message.Replies)
            .Include(m => m.Conversation)
            .Include(m => m.Sender)
            .Include(m => m.Attachments)
            .Where(m =>
                m.MessageStatus == MessageStatus.Sent &&
                m.MessageStatus != MessageStatus.Failed &&
                m.SenderId != userId &&
                (
                    (m.Conversation is PrivateConversation &&
                     ((PrivateConversation)m.Conversation).ReceiverUserId == userId &&
                     groupId == null)
                    ||
                    (m.Conversation is GroupConversation &&
                     context.GroupMembers.Any(gm =>
                         gm.GroupId == ((GroupConversation)m.Conversation).GroupId &&
                         gm.UserId == userId) &&
                     (groupId == null || ((GroupConversation)m.Conversation).GroupId == groupId))
                ))
            .ToListAsync();

        return unreadMessages;
    }

    public async Task<int> GetUnreadMessagesCountAsync(string userId, Guid? groupId = null)
    {
        return await context.Messages
            .AsNoTracking()
            .Include(message => message.Reactions)
            .Include(message => message.Mentions)
            .Include(message => message.Replies)
            .Where(m =>
                m.MessageStatus == MessageStatus.Sent &&
                m.MessageStatus != MessageStatus.Failed &&
                m.SenderId != userId &&
                (
                    (m.Conversation is PrivateConversation &&
                     ((PrivateConversation)m.Conversation).ReceiverUserId == userId &&
                     groupId == null)
                    ||
                    (m.Conversation is GroupConversation &&
                     context.GroupMembers.Any(gm =>
                         gm.GroupId == ((GroupConversation)m.Conversation).GroupId &&
                         gm.UserId == userId) &&
                     (groupId == null || ((GroupConversation)m.Conversation).GroupId == groupId))
                ))
            .CountAsync();
    }

    public async Task<IEnumerable<Message>> SearchMessagesAsync(
        string searchTerm, Guid? conversationId = null)
        => await context.Messages
            .AsNoTracking()
            .Include(message => message.Reactions)
            .Include(message => message.Mentions)
            .Include(message => message.Replies)
            .Include(m => m.Conversation)
            .ThenInclude(c => c as PrivateConversation)
            .Include(m => m.Attachments)
            .Where(m => !string.IsNullOrEmpty(m.Content) &&
                        m.Content.ToLower().Contains(searchTerm.ToLower()) &&
                        (conversationId == null || m.ConversationId == conversationId) &&
                        (m.Conversation is PrivateConversation || m.Conversation is GroupConversation))
            .ToListAsync();
}