using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Requirements;

namespace Sociam.Application.Authorization.Helpers;

public static class StoryPolicies
{
    public const string ViewStory = "StoryPolicy_ViewStory";
    public const string EditStory = "StoryPolicy_EditStory";
    public const string DeleteStory = "StoryPolicy_DeleteStory";
    public const string ManageStoryViewers = "StoryPolicy_ManageViewers";
    public const string React = "StoryPolicy_React";
    public const string Comment = "StoryPolicy_Comment";
    public const string ViewStatistics = "StoryPolicy_ViewStatistics";

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


        options.AddPolicy(Comment,
            policy => policy.Requirements.Add(new StoryOperationRequirement(StoryOperation.Comment)));


        options.AddPolicy(React,
            policy => policy.Requirements.Add(new StoryOperationRequirement(StoryOperation.React)));


        options.AddPolicy(ViewStatistics,
            policy => policy.Requirements.Add(new StoryOperationRequirement(StoryOperation.ViewStatistics)));
    }

}