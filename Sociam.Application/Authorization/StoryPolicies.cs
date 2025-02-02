using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Requirements;

namespace Sociam.Application.Authorization;

public static class StoryPolicies
{
    public const string ViewStory = "StoryPolicy_ViewStory";
    public const string EditStory = "StoryPolicy_EditStory";
    public const string DeleteStory = "StoryPolicy_DeleteStory";
    public const string ManageStoryViewers = "StoryPolicy_ManageViewers";

    public static void AddStoryPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(ViewStory,
            policy => policy.Requirements.Add(new StoryOperationRequirement(StoryOperation.View)));

        options.AddPolicy(EditStory,
            policy => policy.Requirements.Add(new StoryOperationRequirement(StoryOperation.Edit)));

        options.AddPolicy(DeleteStory,
            policy => policy.Requirements.Add(new StoryOperationRequirement(StoryOperation.Delete)));

        options.AddPolicy(ManageStoryViewers,
            policy => policy.Requirements.Add(new StoryOperationRequirement(StoryOperation.ManageViewers)));
    }

}