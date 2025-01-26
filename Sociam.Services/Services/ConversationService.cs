using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Conversation;
using Sociam.Application.Features.Conversations.Commands.CreateGroupConversation;
using Sociam.Application.Features.Conversations.Commands.DeleteConversation;
using Sociam.Application.Features.Conversations.Commands.StartConversation;
using Sociam.Application.Features.Conversations.Queries.GetConversation;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Interfaces;
using System.Net;

namespace Sociam.Services.Services;
public sealed class ConversationService(
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser,
    UserManager<ApplicationUser> userManager,
    IMapper mapper) : IConversationService
{
    public async Task<Result<Guid>> CreateGroupConversationAsync(
        CreateGroupConversationCommand command)
    {
        var existedGroup = await unitOfWork.Repository<Group>()!.GetByIdAsync(command.GroupId);

        if (existedGroup == null)
            return Result<Guid>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, command.GroupId));

        var conversation = new GroupConversation { Id = Guid.NewGuid(), GroupId = command.GroupId };
        unitOfWork.Repository<GroupConversation>()?.Create(conversation);
        await unitOfWork.SaveChangesAsync();
        return Result<Guid>.Success(conversation.Id);
    }

    public async Task<Result<bool>> DeleteConversationAsync(DeleteConversationCommand deleteConversationCommand)
    {
        var existedConversation = await unitOfWork.ConversationRepository.GetByIdAsync(deleteConversationCommand.ConversationId);
        if (existedConversation is not null)
        {
            unitOfWork.ConversationRepository.Delete(existedConversation);
            await unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true, AppConstants.Conversation.ConversationDeleted);
        }

        return Result<bool>.Failure(HttpStatusCode.NotFound);
    }

    public async Task<Result<ConversationDto>> GetConversationBetweenAsync(
        GetConversationQuery getConversationQuery)
    {
        var sender = await userManager.FindByIdAsync(getConversationQuery.SenderId);
        var receiver = await userManager.FindByIdAsync(getConversationQuery.ReceiverId);
        if (sender == null || receiver == null)
            return Result<ConversationDto>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UnkownUser);

        var existedConversation = await unitOfWork.ConversationRepository.GetPrivateConversationBetweenAsync(
            senderId: getConversationQuery.SenderId,
            receiverId: getConversationQuery.ReceiverId);

        if (existedConversation == null)
            return Result<ConversationDto>.Failure(HttpStatusCode.NotFound,
                string.Format(
                    DomainErrors.Conversation.NoConversationBetweenThem,
                    string.Concat(sender.FirstName, " ", sender.LastName),
                    string.Concat(receiver.FirstName, " ", receiver.LastName)));

        var mappedConversation = mapper.Map<ConversationDto>(existedConversation);
        return Result<ConversationDto>.Success(mappedConversation);
    }

    public async Task<Result<string>> StartConversationAsync(StartConversationCommand command)
    {
        var sender = await userManager.FindByIdAsync(currentUser.Id);
        if (sender == null)
            return Result<string>.Failure(HttpStatusCode.Unauthorized, DomainErrors.Users.UserUnauthorized);

        var receiver = await userManager.FindByIdAsync(command.ReceiverId);
        if (receiver == null)
            return Result<string>.Failure(HttpStatusCode.NotFound, DomainErrors.Users.UserNotExists);

        if (currentUser.Id == command.ReceiverId)
            return Result<string>.Failure(HttpStatusCode.Conflict,
                DomainErrors.Conversation.CanNotStartConversationToSelf);

        var conversation = new PrivateConversation()
        {
            Id = Guid.NewGuid(),
            ReceiverUserId = command.ReceiverId,
            SenderUserId = currentUser.Id
        };

        unitOfWork.Repository<Conversation>()?.Create(conversation);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(conversation.Id.ToString());
    }
}
