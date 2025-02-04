using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Authorization;
using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.Features.Groups.Commands.AddUserToGroup;
using Sociam.Application.Features.Groups.Commands.CreateNewGroup;
using Sociam.Application.Features.Groups.Commands.HandleJoinRequest;
using Sociam.Application.Features.Groups.Commands.JoinGroup;
using Sociam.Application.Features.Groups.Commands.RemoveMember;
using Sociam.Application.Features.Groups.Commands.SendGroupMessage;
using Sociam.Application.Features.Groups.Commands.UpdateMemberRole;
using Sociam.Application.Features.Groups.Queries.GetGroup;
using Sociam.Application.Features.Groups.Queries.GetGroupsWithParams;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Interfaces.DataTransferObjects;
using Sociam.Domain.Specifications;
using System.Net;

namespace Sociam.Services.Services;
// Todo: Add Realtime Notification
public sealed class GroupService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser,
    IFileService fileService,
    IHubContext<GroupsHub> hubContext,
    UserManager<ApplicationUser> userManager,
    IValidator<AddUserToGroupCommand> userAddToGroupValidator,
    IValidator<RemoveMemberCommand> removeMemberValidator,
    IAuthorizationService authorizationService) : IGroupService
{
    public async Task<Result<bool>> AddUserToGroupAsync(AddUserToGroupCommand command)
    {
        await userAddToGroupValidator.ValidateAndThrowAsync(command);
        var existedGroup = await GetGroupAsync(command.GroupId);

        // check the member is already added in the group
        bool isMember = await IsMemberInGroupAsync(
            groupId: command.GroupId,
            memberId: command.UserId.ToString());

        if (isMember)
            return Result<bool>.Failure(
                HttpStatusCode.Conflict,
                string.Format(DomainErrors.Group.ItsMemberYet, command.UserId));

        existedGroup!.Members.Add(
            new GroupMember
            {
                Id = Guid.NewGuid(),
                AddedById = currentUser.Id,
                GroupId = command.GroupId,
                UserId = command.UserId.ToString(),
                Role = GroupMemberRole.Member
            });
        var affectedRows = await unitOfWork.SaveChangesAsync();
        var addedUser = await userManager.FindByIdAsync(command.UserId.ToString());
        var addedByUser = await userManager.FindByIdAsync(currentUser.Id);
        if (affectedRows > 0)
        {
            var addedBy = $"{addedByUser!.FirstName} {addedByUser.LastName}";

            await hubContext.Clients.User(command.UserId.ToString()).SendAsync("ReceiveAddedToGroup", $"{addedBy} added you to {existedGroup.Name}");

            return Result<bool>.Success(true, string.Format(AppConstants.Group.UserAddedToGroup, $"{addedUser!.FirstName} {addedUser.LastName}", existedGroup.Name, $"{addedBy}"));
        }

        return Result<bool>.Failure(HttpStatusCode.BadRequest);
    }

    public async Task<Result<Guid>> CreateNewGroupAsync(CreateNewGroupCommand command)
    {
        var validator = new CreateNewGroupCommandValidator();

        await validator.ValidateAndThrowAsync(command);

        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            CreatedByUserId = currentUser.Id,
            GroupPrivacy = command.GroupPrivacy
        };

        var (upload, groupPictureName) = await fileService.UploadFileAsync(command.GroupPictureUrl, "Groups");

        if (upload) group.PictureName = groupPictureName;

        group.Members.Add(
            new GroupMember
            {
                Id = Guid.NewGuid(),
                UserId = currentUser.Id,
                Role = GroupMemberRole.Admin,
                GroupId = group.Id,
                AddedById = currentUser.Id
            });

        unitOfWork.Repository<Group>()?.Create(group);

        await unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(group.Id);
    }

    public async Task<Result<IReadOnlyList<GroupListDto>>> GetAllGroupsAsync()
    {
        var allGroups = await unitOfWork.Repository<Group>()!.GetAllAsync();
        var mappedGroups = mapper.Map<IReadOnlyList<GroupListDto>>(allGroups);
        return Result<IReadOnlyList<GroupListDto>>.Success(mappedGroups);
    }

    public async Task<Result<GroupListDto>> GetGroupByIdAsync(Guid groupId)
    {
        var existedGroup = await GetGroupAsync(groupId);
        var mappedGroup = mapper.Map<GroupListDto>(existedGroup);

        if (mappedGroup is null)
            return Result<GroupListDto>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, groupId));

        return Result<GroupListDto>.Success(mappedGroup);
    }

    public async Task<Result<string>> JoinGroupAsync(JoinGroupCommand command)
    {
        var existedGroup = await GetGroupAsync(command.GroupId);
        if (existedGroup is null)
            return Result<string>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, command.GroupId));

        var isMember = await IsMemberInGroupAsync(existedGroup.Id, currentUser.Id);
        if (isMember)
            return Result<string>.Failure(HttpStatusCode.Conflict, string.Format(DomainErrors.Group.ItsMemberYet, currentUser.Id));

        if (existedGroup.GroupPrivacy == GroupPrivacy.Private)
        {
            var joinRequest = new JoinGroupRequest
            {
                Id = Guid.NewGuid(),
                GroupId = command.GroupId,
                RequestorId = currentUser.Id,
                Status = JoinRequestStatus.Pending
            };
            unitOfWork.Repository<JoinGroupRequest>()!.Create(joinRequest);
            await unitOfWork.SaveChangesAsync();
            return Result<string>.Success(AppConstants.JoinRequest.JoinRequestSent);
        }

        var groupMember = new GroupMember
        {
            Id = Guid.NewGuid(),
            GroupId = command.GroupId,
            UserId = currentUser.Id,
            Role = GroupMemberRole.Member,
            AddedById = currentUser.Id
        };

        unitOfWork.Repository<GroupMember>()!.Create(groupMember);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(AppConstants.JoinRequest.JoinedGroupSuccessfully);
    }

    public async Task<Result<string>> ManageJoinGroupRequestAsync(HandleJoinRequestCommand command)
    {
        var existedRequest = await unitOfWork.Repository<JoinGroupRequest>()?.GetByIdAsync(command.RequestId)!;
        var existedGroup = await GetGroupAsync(existedRequest!.GroupId);

        if (existedRequest is null || existedGroup is null)
            return Result<string>.Failure(HttpStatusCode.BadRequest, DomainErrors.JoinRequest.JoinRequestOrGroupNotFound);

        bool isAdmin = await unitOfWork.GroupMemberRepository.IsAdminInGroupAsync(command.GroupId, currentUser.Id);

        if (!isAdmin)
            return Result<string>.Failure(HttpStatusCode.Forbidden, DomainErrors.JoinRequest.NotAllowed);

        existedRequest.Status = command.JoinStatus;

        if (command.JoinStatus == JoinRequestStatus.Approved)
        {
            var groupMember = new GroupMember
            {
                Id = Guid.NewGuid(),
                GroupId = command.GroupId,
                UserId = existedRequest.RequestorId,
                Role = GroupMemberRole.Member,
                AddedById = currentUser.Id
            };
            unitOfWork.Repository<GroupMember>()!.Create(groupMember);
        }

        await unitOfWork.SaveChangesAsync();

        await NotifyRequestorWithJoinRequestStatusAsync(existedRequest.RequestorId, existedRequest.Status);

        return Result<string>.Success(AppConstants.JoinRequest.JoinRequestHandled);

    }

    private async Task NotifyRequestorWithJoinRequestStatusAsync(string requestorId, JoinRequestStatus status)
    {
        if (status == JoinRequestStatus.Pending)
            return;

        switch (status)
        {
            case JoinRequestStatus.Approved:
                await hubContext.Clients.User(requestorId).SendAsync("ReceiveJoinRequestStatus", AppConstants.JoinRequest.JoinRequestAccepted);
                break;
            case JoinRequestStatus.Rejected:
                await hubContext.Clients.User(requestorId).SendAsync("ReceiveJoinRequestStatus", AppConstants.JoinRequest.JoinRequestRejected);
                break;
            default:
                break;
        }
    }

    public async Task<Result<GroupListDto>> MeGetGroupAsync(GetGroupQuery query)
    {
        var validator = new GetGroupQueryValidator();
        await validator.ValidateAndThrowAsync(query);

        var existedGroup = await GetGroupAsync(query.GroupId);

        if (existedGroup is null)
            return Result<GroupListDto>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, query.GroupId));

        var authResult = await authorizationService.AuthorizeAsync(currentUser.GetUser()!, existedGroup, GroupPolicies.ViewGroup);

        if (!authResult.Succeeded)
            return Result<GroupListDto>.Failure(HttpStatusCode.Forbidden);

        var mappedGroup = mapper.Map<GroupListDto>(existedGroup);

        return Result<GroupListDto>.Success(mappedGroup);
    }

    public async Task<Result<bool>> RemoveMemberFromGroupAsync(RemoveMemberCommand command)
    {
        await removeMemberValidator.ValidateAndThrowAsync(command);

        var existedGroup = await GetGroupAsync(command.GroupId);
        if (existedGroup is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, command.GroupId));

        var isExistedMember = await IsMemberInGroupAsync(command.GroupId, command.MemberId.ToString());

        if (!isExistedMember)
            return Result<bool>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.NotMember, command.MemberId));

        var authResult = await authorizationService.AuthorizeAsync(currentUser.GetUser()!, existedGroup, GroupPolicies.ManageMembers);

        if (!authResult.Succeeded)
            return Result<bool>.Failure(HttpStatusCode.Forbidden);

        var specification = new GetGroupMemberSpecification(command.GroupId, command.MemberId);
        var member = await unitOfWork.Repository<GroupMember>()?.GetBySpecificationAsync(specification)!;

        unitOfWork.Repository<GroupMember>()?.Delete(member!);

        await unitOfWork.SaveChangesAsync();

        await hubContext.Clients.User(command.MemberId.ToString()).SendAsync("ReceiveRemoveFromGroup", string.Format(AppConstants.Group.RemovedFromGroup, currentUser.FullName, existedGroup.Name));

        return Result<bool>.Success(true, AppConstants.Group.MemberRemovedSuccessfully);
    }

    public async Task<Either<Result<PagedResult<GroupDto>>, Result<IEnumerable<GroupDto>>>>
        GetAllGroupsWithParamsAsync(GetGroupsWithParamsQuery query)
    {
        var specification = new GetAllGroupsByParamsSpecification(query.GroupParams);
        var groups = await unitOfWork.Repository<Group>()?.GetAllWithSpecificationAsync(specification)!;
        var mappedGroups = mapper.Map<IEnumerable<GroupDto>>(groups);

        if (query.GroupParams.EnablePaging)
        {
            var filterationCountSpec = new GroupsWithFilterationCountSpecification(query.GroupParams);
            var count = await unitOfWork.Repository<Group>()?.GetCountWithSpecificationAsync(filterationCountSpec)!;
            return Either<Result<PagedResult<GroupDto>>, Result<IEnumerable<GroupDto>>>.FromLeft(
                Result<PagedResult<GroupDto>>.Success(
                new PagedResult<GroupDto>
                {
                    Page = query.GroupParams.Page,
                    PageSize = query.GroupParams.PageSize,
                    Items = [.. mappedGroups],
                    TotalCount = count
                }));
        }

        return Either<Result<PagedResult<GroupDto>>, Result<IEnumerable<GroupDto>>>.FromRight(Result<IEnumerable<GroupDto>>.Success(mappedGroups));
    }

    // TODO: Notify the group users with the message that sending in that group
    public async Task<Result<Guid>> SendGroupMessageAsync(SendGroupMessageCommand command)
    {
        var existedGroup = await GetGroupAsync(command.GroupId);

        if (existedGroup is null)
            return Result<Guid>.Failure(
                HttpStatusCode.NotFound,
                string.Format(DomainErrors.Group.GroupNotExisted, command.GroupId));

        var existedGroupConversation = new ExistedGroupConversationSpecification(
            groupId: command.GroupId,
            groupConversationId: command.GroupConversationId);

        var existedConversation = await unitOfWork.Repository<GroupConversation>()?
            .GetBySpecificationAsync(existedGroupConversation)!;

        if (existedConversation is null)
            return Result<Guid>.Failure(HttpStatusCode.BadRequest, DomainErrors.Group.InitGroupConversationFirst);

        // check is iam member in the group to send the message
        var isMember = await IsMemberInGroupAsync(command.GroupId, currentUser.Id.ToString());

        if (!isMember)
            return Result<Guid>.Failure(HttpStatusCode.Forbidden);

        var message = new Message
        {
            Id = Guid.NewGuid(),
            GroupConversationId = command.GroupConversationId,
            Content = command.Content,
            SenderId = currentUser.Id,
            ReceiverId = existedGroup.CreatedByUserId,
            MessageStatus = MessageStatus.Sent
        };

        if (command.Attachments != null && command.Attachments.Count != 0)
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


        existedConversation.Messages.Add(message);

        await unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(message.Id);
    }

    public async Task<Result<bool>> UpdateMemberRoleAsync(UpdateMemberRoleCommand command)
    {
        var existedGroup = await GetGroupAsync(command.GroupId);
        if (existedGroup is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, command.GroupId));

        var isMember = await IsMemberInGroupAsync(command.GroupId, command.MemberId.ToString());

        if (!isMember)
            return Result<bool>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.NotMember, command.MemberId));

        var authResult = await authorizationService.AuthorizeAsync(currentUser.GetUser()!, existedGroup, GroupPolicies.ManageMembers);

        if (!authResult.Succeeded)
            return Result<bool>.Failure(HttpStatusCode.Forbidden);

        var specification = new GetGroupMemberSpecification(command.GroupId, command.MemberId);

        var member = await unitOfWork.Repository<GroupMember>()?.GetBySpecificationAsync(specification)!;

        if (member!.Role == GroupMemberRole.Admin && command.Role != GroupMemberRole.Admin)
        {
            var adminsCount = await unitOfWork.GroupMemberRepository
                .GetAdminCountInGroupAsync(command.GroupId);

            if (adminsCount == 1)
                return Result<bool>.Failure(
                    HttpStatusCode.BadRequest, DomainErrors.Group.CannotRemoveLastAdmin);
        }

        member.Role = command.Role;

        await unitOfWork.SaveChangesAsync();

        var currentUserName = currentUser.FullName ?? "Unknown";
        var message = $"You have been assigned the role of {member.Role} by {currentUserName}.";
        await hubContext.Clients.User(command.MemberId.ToString()).SendAsync("ReceiveRoleUpdate", message);

        return Result<bool>.Success(true);
    }

    private async Task<Group?> GetGroupAsync(Guid groupId)
        => await unitOfWork.Repository<Group>()?.GetByIdAsync(groupId)!;

    private async Task<bool> IsMemberInGroupAsync(Guid groupId, string memberId)
        => await unitOfWork.GroupMemberRepository.IsMemberInGroupAsync(groupId, memberId);
}
