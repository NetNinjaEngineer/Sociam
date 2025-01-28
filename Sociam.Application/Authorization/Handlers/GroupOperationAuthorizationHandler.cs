using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Requirements;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Application.Authorization.Handlers
{
    public sealed class GroupOperationAuthorizationHandler(
        IUnitOfWork unitOfWork)
        : AuthorizationHandler<GroupOperationRequirement, Group>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, GroupOperationRequirement requirement, Group resource)
        {
            var currentUserId = context.User?.FindFirst(CustomClaims.Uid)?.Value;
            if (string.IsNullOrEmpty(currentUserId))
            {
                context.Fail();
                return;
            }

            // check the user is the admin of the group
            var isAdmin = await unitOfWork.GroupMemberRepository.IsAdminInGroupAsync(
                groupId: resource.Id,
                memberId: currentUserId);

            // check the user is the creator of the group
            var isCreator = resource.CreatedByUserId == currentUserId;

            switch (requirement.Operation)
            {
                case GroupOperation.View:
                    if (await CanViewGroupAsync(resource, currentUserId))
                        context.Succeed(requirement);
                    break;
                case GroupOperation.Edit:
                    if (isAdmin)
                        context.Succeed(requirement);
                    break;

                case GroupOperation.Delete:
                    if (isCreator)
                        context.Succeed(requirement);
                    break;

                case GroupOperation.ManageMembers:
                    if (isAdmin || isCreator)
                        context.Succeed(requirement);
                    break;

                case GroupOperation.ViewMembers:
                    if (await CanViewMembers(resource, currentUserId))
                        context.Succeed(requirement);
                    break;
            }

        }

        private async Task<bool> CanViewMembers(Group group, string userId)
        {
            if (group.GroupPrivacy == GroupPrivacy.Public)
                return true;

            var isMember = await unitOfWork.GroupMemberRepository.IsMemberInGroupAsync(
                groupId: group.Id,
                memberId: userId);

            return isMember;

        }

        private async Task<bool> CanViewGroupAsync(Group group, string currentUserId)
        {
            return group.GroupPrivacy switch
            {
                GroupPrivacy.Public => true,
                GroupPrivacy.Private => await unitOfWork.GroupMemberRepository.IsMemberInGroupAsync(
                                        groupId: group.Id,
                                        memberId: currentUserId),
                GroupPrivacy.Secret => await unitOfWork.GroupMemberRepository.IsMemberInGroupAsync(
                                        groupId: group.Id,
                                        memberId: currentUserId),
                _ => false,
            };
        }
    }
}
