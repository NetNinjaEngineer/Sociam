using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Attachments;
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
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Hubs.Interfaces;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Specifications;
using System.Net;

namespace Sociam.Services.Services;
public sealed class MessageService(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    IFileService fileService,
    IMapper mapper,
    ICurrentUser currentUser,
    IHubContext<MessageHub, IMessageClient> hubContext) : IMessageService
{
    public async Task<Result<bool>> DeleteMessageAsync(Guid messageId)
    {
        var existedMessage = await unitOfWork.MessageRepository.GetByIdAsync(messageId);
        if (existedMessage == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound);
        unitOfWork.MessageRepository.Delete(existedMessage);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true, AppConstants.Messages.MessageDeleted);
    }

    public async Task<Result<bool>> DeleteMessageInConversationAsync(DeleteMessageInConversationCommand command)
    {
        var existedConversation = await unitOfWork.ConversationRepository.GetByIdAsync(command.ConversationId);
        if (existedConversation == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound,
                string.Format(DomainErrors.Conversation.ConversationNotExisted, command.ConversationId.ToString()));

        var specification = new GetExistedMessageSpecification(command.MessageId, command.ConversationId);
        var existedMessage = await unitOfWork.MessageRepository.GetBySpecificationAsync(specification);
        if (existedMessage == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, DomainErrors.Messages.MessageNotFound);
        unitOfWork.MessageRepository.Delete(existedMessage);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true, AppConstants.Messages.MessageDeleted);

    }

    public async Task<Result<bool>> EditMessageAsync(Guid messageId, string newContent)
    {
        var existedMessage = await unitOfWork.MessageRepository.GetByIdAsync(messageId);
        if (existedMessage == null)
            return Result<bool>.Failure(HttpStatusCode.NotFound);
        existedMessage.Content = newContent;
        existedMessage.UpdatedAt = DateTimeOffset.UtcNow;
        existedMessage.IsEdited = true;
        unitOfWork.MessageRepository.Update(existedMessage);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true, AppConstants.Messages.MessageUpdated);
    }

    public async Task<Result<ConversationDto>> GetConversationMessagesAsync(GetPagedConversationMessagesQuery query)
    {
        var pagedConversationMessages = await unitOfWork.MessageRepository.GetPagedConversationMessagesAsync(
            query.ConversationId, query.PageNumber, query.PageSize);
        if (pagedConversationMessages == null)
            return Result<ConversationDto>.Failure(HttpStatusCode.NotFound);
        var mappedConversationMessages = mapper.Map<ConversationDto>(pagedConversationMessages);
        return Result<ConversationDto>.Success(mappedConversationMessages);
    }

    public async Task<Result<ConversationDto>> GetConversationMessagesAsync(Guid conversationId)
    {
        var existedConversation = await unitOfWork.MessageRepository.GetConversationMessagesAsync(conversationId);

        if (existedConversation == null)
            return Result<ConversationDto>.Failure(HttpStatusCode.NotFound);

        var mappedConversation = mapper.Map<ConversationDto>(existedConversation);
        return Result<ConversationDto>.Success(mappedConversation);
    }

    public async Task<Result<MessageDto>> GetMessageByIdAsync(Guid messageId)
    {
        var specification = new GetExistedMessageSpecification(messageId);
        var existedMessage = await unitOfWork.MessageRepository.GetBySpecificationAsync(specification);

        if (existedMessage == null)
            return Result<MessageDto>.Failure(HttpStatusCode.NotFound);

        var mappedMessage = MessageDto.FromEntity(existedMessage);

        return Result<MessageDto>.Success(mappedMessage);

    }

    public async Task<Result<IEnumerable<MessageDto>>> GetMessagesByDateRangeAsync(GetMessagesByDateRangeQuery query)
    {
        var messagesInRange = await unitOfWork.MessageRepository.GetMessagesByDateRangeAsync(
            query.StartDate, query.EndDate, query.ConversationId);

        var mappedMessages = mapper.Map<IEnumerable<MessageDto>>(messagesInRange);

        return Result<IEnumerable<MessageDto>>.Success(mappedMessages);
    }

    public async Task<Result<int>> GetUnreadMessageCountAsync(GetUnreadMessagesCountQuery query)
    {
        var validator = new GetUnreadMessagesCountQueryValidator();
        await validator.ValidateAndThrowAsync(query);

        return Result<int>.Success(await unitOfWork.MessageRepository.GetUnreadMessagesCountAsync(query.UserId));
    }

    public async Task<Result<IEnumerable<MessageDto>>> GetUnreadMessagesAsync(GetUnreadMessagesQuery query)
    {
        var validator = new GetUnreadMessagesQueryValidator();
        await validator.ValidateAndThrowAsync(query);

        var mappedMessages = mapper.Map<IEnumerable<MessageDto>>(
            await unitOfWork.MessageRepository.GetUnreadMessagesAsync(query.UserId));

        return Result<IEnumerable<MessageDto>>.Success(mappedMessages);
    }

    public async Task<Result<ConversationDto>> GetUserConversationsAsync(GetUserConversationQuery query)
    {
        var existedUser = await userManager.FindByIdAsync(query.UserId);
        if (existedUser == null)
            return Result<ConversationDto>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UnkownUser);
        var conversation = await unitOfWork.ConversationRepository.GetPrivateUserConversationsAsync(query.UserId);
        if (conversation == null)
            return Result<ConversationDto>.Failure(HttpStatusCode.NotFound);
        var mappedConversation = mapper.Map<ConversationDto>(conversation);
        return Result<ConversationDto>.Success(mappedConversation);
    }

    public async Task<Result<bool>> MarkMessageAsReadAsync(MarkMessageAsReadCommand command)
    {
        var existedMessage = await unitOfWork.MessageRepository.GetByIdAsync(command.MessageId);
        if (existedMessage is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, DomainErrors.Messages.MessageNotFound);

        existedMessage.MessageStatus = MessageStatus.Read;
        existedMessage.ReadedAt = DateTimeOffset.UtcNow;
        unitOfWork.MessageRepository.Update(existedMessage);
        await unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true, successMessage: AppConstants.Messages.MessageStatusUpdated);

    }

    public async Task<Result<MessageReplyDto>> ReplyToPrivateMessageAsync(ReplyToMessageCommand command)
    {
        var replyToMessageValidator = new ReplyToMessageCommandValidator();

        await replyToMessageValidator.ValidateAndThrowAsync(command);

        var spec = new GetExistedMessageSpecification(command.MessageId);

        var existedMessage = await unitOfWork.MessageRepository.GetBySpecificationAsync(spec);

        if (existedMessage == null)
            return Result<MessageReplyDto>.Failure(HttpStatusCode.NotFound,
                string.Format(DomainErrors.Messages.MessageNotFoundById, command.MessageId));

        var reply = new MessageReply
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            OriginalMessageId = command.MessageId,
            ReplyStatus = ReplyStatus.Active,
            RepliedById = currentUser.Id
        };

        unitOfWork.Repository<MessageReply>()?.Create(reply);

        await unitOfWork.SaveChangesAsync();

        switch (existedMessage.Conversation)
        {
            case PrivateConversation privateConversation:
                var receiverUserId = privateConversation.ReceiverUserId;
                if (receiverUserId != null && receiverUserId != currentUser.Id)
                    await hubContext.Clients.User(receiverUserId)
                        .ReceiveReplyToMessage(
                            existedMessage.ConversationId,
                            existedMessage.Id,
                            reply.Id);
                break;

            case GroupConversation groupConversation:
                var groupMemberIds = await unitOfWork.GroupMemberRepository.GetGroupMembersIdsAsync(groupConversation.GroupId, currentUser.Id);
                if (groupMemberIds.Any())
                    await hubContext.Clients.Users(groupMemberIds)
                        .ReceiveReplyToMessage(
                            existedMessage.ConversationId,
                            existedMessage.Id,
                            reply.Id);
                break;
        }

        var specification = new GetSpecificReplyWithRelatedEntitiesSpecification(reply.Id);
        var replyWithRelatedEntities = await unitOfWork.Repository<MessageReply>()!.GetBySpecificationAsync(specification);

        var mappedReply = MessageReplyDto.FromEntity(replyWithRelatedEntities!);

        return Result<MessageReplyDto>.Success(mappedReply);
    }

    public async Task<Result<MessageReplyDto>> ReplyToReplyMessageAsync(ReplyToReplyMessageCommand command)
    {
        var replyToReplyValidator = new ReplyToReplyMessageCommandValidator();
        await replyToReplyValidator.ValidateAndThrowAsync(command);

        // Check if the parent reply exists
        var parentSpec = new GetSpecificReplyWithRelatedEntitiesSpecification(command.ParentReplyId);
        var parentReply = await unitOfWork.Repository<MessageReply>()?.GetBySpecificationAsync(parentSpec)!;

        if (parentReply == null)
            return Result<MessageReplyDto>.Failure(HttpStatusCode.NotFound,
                string.Format(DomainErrors.Replies.ReplyNotFound, command.ParentReplyId));

        var reply = new MessageReply
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            OriginalMessageId = parentReply.OriginalMessageId,
            ParentReplyId = command.ParentReplyId,
            ReplyStatus = ReplyStatus.Active,
            RepliedById = currentUser.Id,
            CreatedAt = DateTimeOffset.UtcNow
        };

        unitOfWork.Repository<MessageReply>()?.Create(reply);

        await unitOfWork.SaveChangesAsync();

        var originalMessageSpec = new GetExistedMessageSpecification(parentReply.OriginalMessageId);
        var originalMessage = await unitOfWork.MessageRepository.GetBySpecificationAsync(originalMessageSpec);

        if (originalMessage != null)
        {
            switch (originalMessage.Conversation)
            {
                case PrivateConversation privateConversation:
                    var receiverUserId = privateConversation.ReceiverUserId;
                    if (receiverUserId != null && receiverUserId != currentUser.Id)
                        await hubContext.Clients.User(receiverUserId)
                            .ReceiveReplyToMessage(
                                originalMessage.ConversationId,
                                originalMessage.Id,
                                reply.Id);
                    break;

                case GroupConversation groupConversation:
                    // Get all group members except the current user
                    var groupMemberIds = await unitOfWork.GroupMemberRepository.GetGroupMembersIdsAsync(groupConversation.GroupId, currentUser.Id);

                    if (groupMemberIds.Any())
                        await hubContext.Clients.Users(groupMemberIds)
                            .ReceiveReplyToMessage(
                                originalMessage.ConversationId,
                                originalMessage.Id,
                                reply.Id);
                    break;
            }
        }

        var replySpec = new GetSpecificReplyWithRelatedEntitiesSpecification(reply.Id);
        var replyWithRelatedEntities = await unitOfWork.Repository<MessageReply>()!
            .GetBySpecificationAsync(replySpec);

        var mappedReply = MessageReplyDto.FromEntity(replyWithRelatedEntities!);
        return Result<MessageReplyDto>.Success(mappedReply);
    }

    public async Task<Result<IEnumerable<MessageDto>>> SearchMessagesAsync(SearchMessagesQuery searchMessagesQuery)
    {
        var searchedMessages = await unitOfWork.MessageRepository.SearchMessagesAsync(
            searchTerm: searchMessagesQuery.SearchTerm,
            conversationId: searchMessagesQuery.ConversationId);

        var mappedResult = mapper.Map<IEnumerable<MessageDto>>(searchedMessages);
        return Result<IEnumerable<MessageDto>>.Success(mappedResult);
    }

    public async Task<Result<MessageDto>> SendPrivateMessageAsync(SendPrivateMessageCommand command)
    {
        if (string.Equals(command.SenderId, command.ReceiverId))
            return Result<MessageDto>.Failure(HttpStatusCode.Conflict, DomainErrors.Messages.CanNotSendMessagesToSelf);

        var sender = await userManager.FindByIdAsync(command.SenderId);
        var receiver = await userManager.FindByIdAsync(command.ReceiverId);

        if (sender == null || receiver == null)
            return Result<MessageDto>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UnkownUser);

        var specification = new GetExistedConversationBetweenSenderAndReceiverSpecification(
            command.SenderId,
            command.ReceiverId);

        var conversation = await unitOfWork.Repository<PrivateConversation>()!.GetBySpecificationAndIdAsync(specification, command.ConversationId);

        if (conversation == null)
            return Result<MessageDto>.Failure(HttpStatusCode.NotFound,
                DomainErrors.Conversation.ShouldStartConversation);

        var message = new Message
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            MessageStatus = MessageStatus.Sent,
            ConversationId = conversation.Id,
            SenderId = command.SenderId
        };


        if (command.Attachments?.Count() > 0)
        {
            var uploadResults = await fileService.UploadFilesParallelAsync(command.Attachments);

            foreach (var result in uploadResults)
            {
                message.Attachments.Add(new Attachment
                {
                    Id = Guid.NewGuid(),
                    AttachmentSize = result.Size,
                    MessageId = message.Id,
                    Name = result.SavedFileName,
                    AttachmentType = Enum.Parse<AttachmentType>(result.Type.ToString()),
                    Url = result.Url
                });
            }
        }

        unitOfWork.Repository<Message>()?.Create(message);

        conversation.LastMessageAt = message.CreatedAt;

        unitOfWork.Repository<Conversation>()?.Update(conversation);

        await unitOfWork.SaveChangesAsync();

        var messageResult = MessageDto.Create(
            string.Concat(sender.FirstName, " ", sender.LastName),
            sender.Id,
            message.ConversationId,
            MessageStatus.Sent,
            message.Content,
            message.Attachments.Count > 0 ? mapper.Map<List<AttachmentDto>>(message.Attachments) : [],
            null);

        await hubContext.Clients.User(command.ReceiverId).ReceivePrivateMessage(messageResult);

        return Result<MessageDto>.Success(messageResult);
    }

    public async Task<Result<MessageDto>> SendPrivateMessageByCurrentUserAsync(
        SendPrivateMessageByCurrentUserCommand command)
    {
        if (string.Equals(currentUser.Id, command.ReceiverId))
            return Result<MessageDto>.Failure(HttpStatusCode.Conflict, DomainErrors.Messages.CanNotSendMessagesToSelf);

        var sender = await userManager.FindByIdAsync(currentUser.Id);
        var receiver = await userManager.FindByIdAsync(command.ReceiverId);

        if (sender == null || receiver == null)
            return Result<MessageDto>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UnkownUser);

        var specification = new GetExistedConversationBetweenSenderAndReceiverSpecification(
            currentUser.Id,
            command.ReceiverId);

        var conversation = await unitOfWork.Repository<PrivateConversation>()!.GetBySpecificationAndIdAsync(specification, command.ConversationId);

        if (conversation == null)
            return Result<MessageDto>.Failure(HttpStatusCode.NotFound,
                DomainErrors.Conversation.ShouldStartConversation);

        var message = new Message
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            MessageStatus = MessageStatus.Sent,
            ConversationId = conversation.Id,
            SenderId = currentUser.Id
        };

        if (command.Attachments?.Count() > 0)
        {
            var uploadResults = await fileService.UploadFilesParallelAsync(command.Attachments);

            foreach (var result in uploadResults)
            {
                message.Attachments.Add(new Attachment
                {
                    Id = Guid.NewGuid(),
                    AttachmentSize = result.Size,
                    MessageId = message.Id,
                    Name = result.SavedFileName,
                    AttachmentType = Enum.Parse<AttachmentType>(result.Type.ToString()),
                    Url = result.Url
                });
            }
        }

        unitOfWork.Repository<Message>()?.Create(message);

        conversation.LastMessageAt = message.CreatedAt;

        unitOfWork.Repository<Conversation>()?.Update(conversation);

        await unitOfWork.SaveChangesAsync();

        var messageResult = MessageDto.Create(
            string.Concat(sender.FirstName, " ", sender.LastName),
            message.SenderId,
            message.ConversationId,
            MessageStatus.Sent,
            message.Content,
            message.Attachments.Count > 0 ? mapper.Map<List<AttachmentDto>>(message.Attachments) : [],
            null);

        await hubContext.Clients.User(command.ReceiverId).ReceivePrivateMessage(messageResult);

        var mappedMessage = MessageDto.FromEntity(message);

        return Result<MessageDto>.Success(mappedMessage);
    }
}
