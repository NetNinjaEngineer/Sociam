using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Requirements;

namespace Sociam.Application.Authorization.Helpers;

public static class PostPolicies
{
    private const string PolicyPrefix = "PostPolicy_";

    public const string EditPost = $"{PolicyPrefix}EditPost";

    public const string ReactToPost = $"{PolicyPrefix}_ReactToPost";

    public const string ChangePostPrivacy = $"{PolicyPrefix}ChangePostPrivacy";

    public static void AddPostPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(EditPost, policy => policy.Requirements.Add(new PostOperationsRequirement(PostOperations.Edit)));

        options.AddPolicy(ReactToPost, policy => policy.Requirements.Add(new PostOperationsRequirement(PostOperations.React)));

        options.AddPolicy(ChangePostPrivacy, policy => policy.Requirements.Add(new PostOperationsRequirement(PostOperations.ChangePrivacy)));
    }
}