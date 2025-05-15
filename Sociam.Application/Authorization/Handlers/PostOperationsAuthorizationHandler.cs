using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Helpers;
using Sociam.Application.Authorization.Requirements;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Enums;
using Sociam.Domain.Interfaces;

namespace Sociam.Application.Authorization.Handlers;

public sealed class PostOperationsAuthorizationHandler(
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork)
    : AuthorizationHandler<PostOperationsRequirement, Post>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PostOperationsRequirement requirement,
        Post post)
    {
        if (string.IsNullOrWhiteSpace(currentUser.Id))
        {
            context.Fail();
            return;
        }

        switch (requirement.PostOperations)
        {
            case PostOperations.EDIT:
                await HandleEditOperationAsync(context, requirement, post);
                break;

            case PostOperations.REACT:
                await HandleReactOperationAsync(context, requirement, post);
                break;

            default:
                context.Fail(new AuthorizationFailureReason(this, "Unsupported post operation."));
                break;
        }
    }

    private async Task HandleEditOperationAsync(
        AuthorizationHandlerContext context,
        PostOperationsRequirement requirement,
        Post post)
    {
        var canEdit = await CanEditPostAsync(post, currentUser.Id, currentUser.GetUser()!);
        if (canEdit)
            context.Succeed(requirement);
        else
            context.Fail(new AuthorizationFailureReason(this, "You are not authorized to edit this post."));
    }

    private async Task HandleReactOperationAsync(
        AuthorizationHandlerContext context,
        PostOperationsRequirement requirement,
        Post post)
    {
        if (post.CreatedById == currentUser.Id || post.Privacy == PostPrivacy.Public)
        {
            context.Succeed(requirement);
            return;
        }

        switch (post.Privacy)
        {
            case PostPrivacy.Friends:
                if (await unitOfWork.FriendshipRepository.AreFriendsAsync(currentUser.Id, post.CreatedById))
                    context.Succeed(requirement);
                else
                    context.Fail(new AuthorizationFailureReason(this, "You must be a friend of the post creator to react."));
                break;

            case PostPrivacy.OnlyMe:
                if (post.CreatedById == currentUser.Id)
                    context.Succeed(requirement);
                else
                    context.Fail(new AuthorizationFailureReason(this, "Only the post creator can react to this post."));
                break;

            default:
                context.Fail(new AuthorizationFailureReason(this, "Invalid privacy setting."));
                break;
        }
    }

    private static Task<bool> CanEditPostAsync(Post post, string currentUserId, ClaimsPrincipal user)
    {
        var isAdmin = user.IsInRole(AppConstants.Roles.Admin);
        var isCreator = post.CreatedById == currentUserId;
        var isWithinEditableTime = (DateTimeOffset.UtcNow - post.CreatedAt).TotalHours <= 24;

        return Task.FromResult(isCreator && isAdmin && isWithinEditableTime);
    }
}
