using Sociam.Application.Bases;
using Sociam.Application.DTOs.Groups;
using Sociam.Application.Features.Groups.Commands.AddUserToGroup;
using Sociam.Application.Features.Groups.Commands.CreateNewGroup;
using Sociam.Application.Features.Groups.Commands.HandleJoinRequest;
using Sociam.Application.Features.Groups.Commands.JoinGroup;
using Sociam.Application.Features.Groups.Commands.RemoveMember;
using Sociam.Application.Features.Groups.Commands.SendGroupMessage;
using Sociam.Application.Features.Groups.Queries.GetGroup;
using Sociam.Application.Features.Groups.Queries.GetGroupsWithParams;
using Sociam.Application.Helpers;

namespace Sociam.Application.Interfaces.Services;
public interface IGroupService
{
    Task<Result<Guid>> CreateNewGroupAsync(CreateNewGroupCommand command);
    Task<Result<bool>> AddUserToGroupAsync(AddUserToGroupCommand command);
    Task<Result<IReadOnlyList<GroupListDto>>> GetAllGroupsAsync();
    Task<Either<Result<PagedResult<GroupDto>>, Result<IEnumerable<GroupDto>>>> GetAllGroupsWithParamsAsync(GetGroupsWithParamsQuery query);
    Task<Result<GroupListDto>> GetGroupByIdAsync(Guid groupId);
    Task<Result<string>> JoinGroupAsync(JoinGroupCommand command);
    Task<Result<GroupListDto>> MeGetGroupAsync(GetGroupQuery query);
    Task<Result<string>> ManageJoinGroupRequestAsync(HandleJoinRequestCommand command);
    Task<Result<bool>> RemoveMemberFromGroupAsync(RemoveMemberCommand command);
    Task<Result<Guid>> SendGroupMessageAsync(SendGroupMessageCommand command);
}
