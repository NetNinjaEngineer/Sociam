using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Requirements;
using Sociam.Application.Helpers;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Application.Authorization.Handlers;

public sealed class StoryOperationAuthorizationHandler(IUnitOfWork unitOfWork)
    : AuthorizationHandler<StoryOperationRequirement, Story>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, StoryOperationRequirement requirement, Story resource)
    {
        var currentUserId = context.User?.FindFirst(CustomClaims.Uid)?.Value;

        if (string.IsNullOrEmpty(currentUserId))
        {
            context.Fail();
            return;
        }

        // Creator Can Edit, Delete And Manage Story Viewers
        var isCreator = resource.UserId == currentUserId;

        switch (requirement)
        {
            case { StoryOperation: StoryOperation.View }:
                if (await CanViewStoryAsync(resource, currentUserId))
                    context.Succeed(requirement);
                break;

            case { StoryOperation: StoryOperation.Delete }:
            case { StoryOperation: StoryOperation.Edit }:
            case { StoryOperation: StoryOperation.ManageViewers }:
                if (isCreator)
                    context.Succeed(requirement);
                break;
        }

    }

    private async Task<bool> CanViewStoryAsync(Story resource, string currentUserId)
    {
        return resource.StoryPrivacy switch
        {
            // Story Is Public To All Users In Sociam Platform
            StoryPrivacy.Public => true,
            // Only Friends Can View The Story
            StoryPrivacy.Friends => await unitOfWork.FriendshipRepository.AreFriendsAsync(
                user1Id: resource.UserId,
                user2Id: currentUserId),
            StoryPrivacy.Private => resource.UserId == currentUserId,
            // Custom Friends Can View The Story
            StoryPrivacy.Custom => await unitOfWork.StoryViewRepository.IsSetAsStoryViewerAsync(resource.Id, currentUserId),
            _ => false
        };
    }
}