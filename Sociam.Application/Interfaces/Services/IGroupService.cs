using Sociam.Application.Bases;
using Sociam.Application.Features.Groups.Commands.AddUserToGroup;
using Sociam.Application.Features.Groups.Commands.CreateNewGroup;

namespace Sociam.Application.Interfaces.Services;
public interface IGroupService
{
    Task<Result<Guid>> CreateNewGroupAsync(CreateNewGroupCommand command);

    Task<Result<bool>> AddUserToGroupAsync(AddUserToGroupCommand command);
    //Task<Result<bool>> RemoveUserFromGroupAsync(RemoveUserFromGroupCommand command);
    //Task<Result<MessageDto>> SendGroupMessageAsync(SendGroupMessageCommand command);
    //Task<Result<bool>> UpdateGroupMetadataAsync(UpdateGroupMetadataCommand command);
}
