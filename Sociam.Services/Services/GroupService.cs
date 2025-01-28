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
using Sociam.Application.Features.Groups.Commands.JoinGroup;
using Sociam.Application.Features.Groups.Queries.GetGroup;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using System.Net;

namespace Sociam.Services.Services;
// Todo: Add Realtime Notification
public sealed class GroupService(
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser,
    IFileService fileService,
    IValidator<AddUserToGroupCommand> userAddToGroupValidator,
    UserManager<ApplicationUser> userManager,
    IHubContext<GroupsHub> hubContext,
    IMapper mapper,
    IAuthorizationService authorizationService) : IGroupService
{
    public async Task<Result<bool>> AddUserToGroupAsync(AddUserToGroupCommand command)
    {
        await userAddToGroupValidator.ValidateAndThrowAsync(command);
        var existedGroup = await unitOfWork.Repository<Group>()?.GetByIdAsync(command.GroupId)!;

        // check the member is already added in the group
        bool isMember = await unitOfWork.GroupMemberRepository.IsMemberInGroupAsync(
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
        var existedGroup = await unitOfWork.Repository<Group>()!.GetByIdAsync(groupId);
        var mappedGroup = mapper.Map<GroupListDto>(existedGroup);

        if (mappedGroup is null)
            return Result<GroupListDto>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, groupId));

        return Result<GroupListDto>.Success(mappedGroup);
    }

    public async Task<Result<string>> JoinGroupAsync(JoinGroupCommand command)
    {
        var existedGroup = await unitOfWork.Repository<Group>()!.GetByIdAsync(command.GroupId);
        if (existedGroup is null)
            return Result<string>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, command.GroupId));

        var isMember = await unitOfWork.GroupMemberRepository.IsMemberInGroupAsync(existedGroup.Id, currentUser.Id);
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

    public async Task<Result<GroupListDto>> MeGetGroupAsync(GetGroupQuery query)
    {
        var validator = new GetGroupQueryValidator();
        await validator.ValidateAndThrowAsync(query);

        var existedGroup = await unitOfWork.Repository<Group>()?.GetByIdAsync(query.GroupId)!;

        if (existedGroup is null)
            return Result<GroupListDto>.Failure(HttpStatusCode.NotFound, string.Format(DomainErrors.Group.GroupNotExisted, query.GroupId));

        var user = currentUser.GetUser();

        if (user is null)
            return Result<GroupListDto>.Failure(HttpStatusCode.Unauthorized);

        var authResult = await authorizationService.AuthorizeAsync(user, existedGroup, GroupPolicies.ViewGroup);

        if (!authResult.Succeeded)
            return Result<GroupListDto>.Failure(HttpStatusCode.Forbidden);

        var mappedGroup = mapper.Map<GroupListDto>(existedGroup);

        return Result<GroupListDto>.Success(mappedGroup);
    }
}
