using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.DTOs.Replies;
using Sociam.Application.Features.Conversations.Queries.GetPagedConversationMessages;
using Sociam.Application.Features.Conversations.Queries.GetUserConversation;
using Sociam.Application.Features.Messages.Commands.DeleteMessageInConversation;
using Sociam.Application.Features.Messages.Commands.MarkMessageAsRead;
using Sociam.Application.Features.Messages.Commands.ReplyToMessage;
using Sociam.Application.Features.Messages.Commands.ReplyToReplyMessage;
using Sociam.Application.Features.Messages.Commands.SendPrivateMessage;
using Sociam.Application.Features.Messages.Commands.SendPrivateMessageByCurrentUser;
using Sociam.Application.Features.Messages.Queries.GetMessagesByDateRange;
using Sociam.Application.Features.Messages.Queries.GetUnreadMessages;
using Sociam.Application.Features.Messages.Queries.GetUnreadMessagesCount;
using Sociam.Application.Features.Messages.Queries.SearchMessages;

namespace Sociam.Application.Interfaces.Services;
public interface IMessageService
{
    Task<Result<MessageDto>> SendPrivateMessageAsync(SendPrivateMessageCommand command);
    Task<Result<MessageDto>> SendPrivateMessageByCurrentUserAsync(SendPrivateMessageByCurrentUserCommand command);
    // Retrieve message methods
    Task<Result<ConversationDto>> GetConversationMessagesAsync(GetPagedConversationMessagesQuery query);
    Task<Result<ConversationDto>> GetConversationMessagesAsync(Guid conversationId);
    Task<Result<ConversationDto>> GetUserConversationsAsync(GetUserConversationQuery query);
    Task<Result<MessageDto>> GetMessageByIdAsync(Guid messageId);

    // Message status and management methods
    Task<Result<bool>> MarkMessageAsReadAsync(MarkMessageAsReadCommand command);
    Task<Result<int>> GetUnreadMessageCountAsync(GetUnreadMessagesCountQuery query);
    Task<Result<IEnumerable<MessageDto>>> GetUnreadMessagesAsync(GetUnreadMessagesQuery query);
    Task<Result<bool>> DeleteMessageAsync(Guid messageId);
    Task<Result<bool>> DeleteMessageInConversationAsync(DeleteMessageInConversationCommand command);
    Task<Result<bool>> EditMessageAsync(Guid messageId, string newContent);

    // Search and filtering methods
    Task<Result<IEnumerable<MessageDto>>> SearchMessagesAsync(SearchMessagesQuery searchMessagesQuery);
    Task<Result<IEnumerable<MessageDto>>> GetMessagesByDateRangeAsync(GetMessagesByDateRangeQuery query);

    Task<Result<MessageReplyDto>> ReplyToPrivateMessageAsync(ReplyToMessageCommand command);
    Task<Result<MessageReplyDto>> ReplyToReplyMessageAsync(ReplyToReplyMessageCommand command);


    //// Message Interaction Features
    //Task<Result<bool>> ReactToMessageAsync(ReactToMessageCommand command);
    //Task<Result<IEnumerable<MessageReactionDto>>> GetMessageReactionsAsync(Guid messageId);

    //// Advanced Search and Filtering
    //Task<Result<IEnumerable<MessageDto>>> SearchMessagesWithFiltersAsync(SearchMessagesWithFiltersQuery query);
    //Task<Result<IEnumerable<ConversationDto>>> SearchConversationsAsync(SearchConversationsQuery query);


    //// Message Media and Rich Content
    //Task<Result<IEnumerable<MediaDto>>> GetMessageMediaAsync(Guid messageId);

    //// Message Pinning and Important Messages
    //Task<Result<bool>> PinMessageInConversationAsync(PinMessageCommand command);
    //Task<Result<bool>> UnpinMessageInConversationAsync(UnpinMessageCommand command);
    //Task<Result<IEnumerable<MessageDto>>> GetPinnedMessagesAsync(Guid conversationId);

    //// Conversation Insights and Analytics
    //Task<Result<ConversationAnalyticsDto>> GetConversationAnalyticsAsync(Guid conversationId);
    //Task<Result<UserCommunicationStatsDto>> GetUserCommunicationStatsAsync(string userId);
}
