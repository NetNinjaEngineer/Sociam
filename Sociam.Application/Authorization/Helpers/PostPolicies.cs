using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Requirements;

namespace Sociam.Application.Authorization.Helpers;

public static class PostPolicies
{
    public const string EditPost = "PostPolicy_EditPost";
    public const string ReactToPost = "PostPolicy_ReactToPost";
    public static void AddPostPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(EditPost, policy =>
            policy.Requirements.Add(new PostOperationsRequirement(PostOperations.EDIT)));

        options.AddPolicy(ReactToPost, policy =>
            policy.Requirements.Add(new PostOperationsRequirement(PostOperations.REACT)));
    }
}