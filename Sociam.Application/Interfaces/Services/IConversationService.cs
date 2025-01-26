using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.Features.Conversations.Commands.CreateGroupConversation;
using Sociam.Application.Features.Conversations.Commands.DeleteConversation;
using Sociam.Application.Features.Conversations.Commands.StartConversation;
using Sociam.Application.Features.Conversations.Queries.GetConversation;

namespace Sociam.Application.Interfaces.Services;
public interface IConversationService
{
    Task<Result<string>> StartConversationAsync(StartConversationCommand command);
    Task<Result<ConversationDto>> GetConversationBetweenAsync(GetConversationQuery getConversationQuery);
    Task<Result<bool>> DeleteConversationAsync(DeleteConversationCommand deleteConversationCommand);
    Task<Result<Guid>> CreateGroupConversationAsync(CreateGroupConversationCommand createGroupConversationCommand);
}
