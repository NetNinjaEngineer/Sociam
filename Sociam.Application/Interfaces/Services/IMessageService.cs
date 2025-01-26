using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.DTOs.Messages;
using Sociam.Application.Features.Conversations.Queries.GetPagedConversationMessages;
using Sociam.Application.Features.Conversations.Queries.GetUserConversation;
using Sociam.Application.Features.Messages.Commands.DeleteMessageInConversation;
using Sociam.Application.Features.Messages.Commands.MarkMessageAsRead;
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

    //// Real-time and notification methods
    ////Task<bool> BlockUserAsync(string blockedUserId);
    ////Task<bool> UnblockUserAsync(string blockedUserId);
    ////Task<IEnumerable<UserDto>> GetBlockedUsersAsync();

    // Search and filtering methods
    Task<Result<IEnumerable<MessageDto>>> SearchMessagesAsync(SearchMessagesQuery searchMessagesQuery);
    Task<Result<IEnumerable<MessageDto>>> GetMessagesByDateRangeAsync(GetMessagesByDateRangeQuery query);

    //Task<Result<MessageDto>> SendPrivateMessageWithAttachmentAsync(SendPrivateMessageWithAttachmentCommand command);
    //Task<Result<MessageDto>> ReplyToMessageAsync(ReplyToMessageCommand command);

    //// Advanced Conversation Management
    //Task<Result<bool>> UpdateConversationSettingsAsync(UpdateConversationSettingsCommand command);
    //Task<Result<bool>> MuteConversationAsync(MuteConversationCommand command);
    //Task<Result<bool>> ArchiveConversationAsync(ArchiveConversationCommand command);



    //// Message Interaction Features
    //Task<Result<bool>> ReactToMessageAsync(ReactToMessageCommand command);
    //Task<Result<IEnumerable<MessageReactionDto>>> GetMessageReactionsAsync(Guid messageId);
    //Task<Result<bool>> ForwardMessageAsync(ForwardMessageCommand command);

    //// Advanced Search and Filtering
    //Task<Result<IEnumerable<MessageDto>>> SearchMessagesWithFiltersAsync(SearchMessagesWithFiltersQuery query);
    //Task<Result<IEnumerable<ConversationDto>>> SearchConversationsAsync(SearchConversationsQuery query);

    //// User Interaction and Privacy
    //Task<Result<bool>> BlockUserAsync(BlockUserCommand command);
    //Task<Result<bool>> UnblockUserAsync(UnblockUserCommand command);
    //Task<Result<IEnumerable<UserDto>>> GetBlockedUsersAsync();
    //Task<Result<bool>> SetUserPrivacySettingsAsync(SetUserPrivacySettingsCommand command);

    //// Message Media and Rich Content
    //Task<Result<MessageDto>> SendMessageWithMediaAsync(SendMessageWithMediaCommand command);
    //Task<Result<IEnumerable<MediaDto>>> GetMessageMediaAsync(Guid messageId);

    //// Message Pinning and Important Messages
    //Task<Result<bool>> PinMessageInConversationAsync(PinMessageCommand command);
    //Task<Result<bool>> UnpinMessageInConversationAsync(UnpinMessageCommand command);
    //Task<Result<IEnumerable<MessageDto>>> GetPinnedMessagesAsync(Guid conversationId);

    //// Conversation Insights and Analytics
    //Task<Result<ConversationAnalyticsDto>> GetConversationAnalyticsAsync(Guid conversationId);
    //Task<Result<UserCommunicationStatsDto>> GetUserCommunicationStatsAsync(string userId);

    //// Message Scheduling and Delayed Sending
    //Task<Result<MessageDto>> ScheduleMessageAsync(ScheduleMessageCommand command);
    //Task<Result<bool>> CancelScheduledMessageAsync(Guid scheduledMessageId);
    //Task<Result<IEnumerable<MessageDto>>> GetScheduledMessagesAsync(string userId);

    //// Conversation Export and Backup
    //Task<Result<string>> ExportConversationAsync(ExportConversationCommand command);
    //Task<Result<bool>> ImportConversationAsync(ImportConversationCommand command);
}
