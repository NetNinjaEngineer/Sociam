﻿using Sociam.Domain.Entities;

namespace Sociam.Domain.Interfaces;
public interface IMessageRepository : IGenericRepository<Message>
{
    Task<Conversation?> GetConversationMessagesAsync(Guid conversationId);
    Task<Conversation?> GetPagedConversationMessagesAsync(Guid conversationId, int page, int size);
    Task<IEnumerable<Message>> GetMessagesByDateRangeAsync(DateOnly startDate, DateOnly endDate, Guid? conversationId = null);
    Task<int> GetUnreadMessagesCountAsync(string userId);
    Task<IEnumerable<Message>> GetUnreadMessagesAsync(string userId);
    Task<IEnumerable<Message>> SearchMessagesAsync(string searchTerm, Guid? conversationId = null);
}