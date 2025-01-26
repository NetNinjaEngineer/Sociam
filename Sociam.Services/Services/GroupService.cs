using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Sociam.Application.Bases;
using Sociam.Application.Features.Groups.Commands.AddUserToGroup;
using Sociam.Application.Features.Groups.Commands.CreateNewGroup;
using Sociam.Application.Helpers;
using Sociam.Application.Hubs;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;
using System.Net;

namespace Sociam.Services.Services;
public sealed class GroupService(
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser,
    IFileService fileService,
    IValidator<AddUserToGroupCommand> userAddToGroupValidator,
    UserManager<ApplicationUser> userManager,
    IHubContext<GroupsHub> hubContext) : IGroupService
{
    public async Task<Result<bool>> AddUserToGroupAsync(AddUserToGroupCommand command)
    {
        await userAddToGroupValidator.ValidateAndThrowAsync(command);
        var existedGroup = await unitOfWork.Repository<Group>()?.GetByIdAsync(command.GroupId)!;
        existedGroup!.Members.Add(new GroupMember { AddedById = currentUser.Id, GroupId = command.GroupId, UserId = command.UserId.ToString(), Role = GroupMemberRole.Member });
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

        var group = new Group { Id = Guid.NewGuid(), Name = command.Name, Description = command.Description, CreatedByUserId = currentUser.Id };

        var (upload, groupPictureName) = await fileService.UploadFileAsync(command.GroupPictureUrl, "Images");

        if (upload) group.PictureName = groupPictureName;

        group.Members.Add(
            new GroupMember
            {
                Id = Guid.NewGuid(),
                UserId = currentUser.Id,
                Role = GroupMemberRole.Admin,
                GroupId = group.Id
            });

        unitOfWork.Repository<Group>()?.Create(group);

        await unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(group.Id);
    }
}
