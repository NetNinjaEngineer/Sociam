using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Helpers;
using Sociam.Application.Authorization.Requirements;
using Sociam.Application.Helpers;
using Sociam.Application.Interfaces.Services;
using Sociam.Domain.Entities;

namespace Sociam.Application.Authorization.Handlers
{
    public sealed class PostOperationsAuthorizationHandler(ICurrentUser currentUser) : AuthorizationHandler<PostOperationsRequirement, Post>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PostOperationsRequirement requirement, Post post)
        {
            if (string.IsNullOrEmpty(currentUser.Id))
            {
                context.Fail();
                return;
            }

            switch (requirement)
            {
                // Post Owner and Admins Can Edit The Post
                case { PostOperations: PostOperations.EDIT }:
                    if (await CanEditPostAsync(post, currentUser.Id, currentUser.GetUser()!))
                        context.Succeed(requirement);
                    else
                        context.Fail(new AuthorizationFailureReason(this, "You are not authorized to edit this post."));
                    break;
            }

        }

        private static Task<bool> CanEditPostAsync(Post post, string currentUserId, ClaimsPrincipal user)
        {
            // Authorization: Check if the user is the owner or an admin
            var isAdmin = user.IsInRole(AppConstants.Roles.Admin);
            if (post.CreatedById != currentUserId || !isAdmin)
                return Task.FromResult(false);

            // Restriction: Check if the post is editable (within 24 hours)
            return Task.FromResult(!((DateTimeOffset.UtcNow - post.CreatedAt).TotalHours > 24));
        }
    }
}
