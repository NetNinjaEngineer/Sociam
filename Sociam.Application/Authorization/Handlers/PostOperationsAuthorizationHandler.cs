using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Sociam.Application.Authorization.Helpers;
using Sociam.Application.Authorization.Requirements;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;
using Sociam.Domain.Entities.Identity;

namespace Sociam.Application.Authorization.Handlers
{
    public sealed class PostOperationsAuthorizationHandler(
        ICurrentUser currentUser,
        UserManager<ApplicationUser> userManager) : AuthorizationHandler<PostOperationsRequirement, Post>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PostOperationsRequirement requirement, Post post)
        {
            var currentUserId = currentUser.Id;
            if (string.IsNullOrEmpty(currentUserId))
            {
                context.Fail();
                return;
            }

            switch (requirement)
            {
                // Post Owner and Admins Can Edit The Post
                case { PostOperations: PostOperations.EDIT }:
                    if (await CanEditPostAsync(post, currentUserId))
                        context.Succeed(requirement);
                    else
                        context.Fail(new AuthorizationFailureReason(this, "You are not authorized to edit this post."));
                    break;
            }

        }

        private async Task<bool> CanEditPostAsync(Post post, string currentUserId)
        {
            // Authorization: Check if the user is the owner or an admin
            var appUser = await userManager.FindByIdAsync(currentUserId);
            var isAdmin = await userManager.IsInRoleAsync(appUser!, AppConstants.Roles.Admin);
            if (post.CreatedById != currentUserId || !isAdmin)
                return false;

            // Restriction: Check if the post is editable (within 24 hours)
            if ((DateTimeOffset.UtcNow - post.CreatedAt).TotalHours > 24)
                return false;

            return true;
        }
    }
}
