using Microsoft.AspNetCore.Authorization;
using Sociam.Application.Authorization.Requirements;

namespace Sociam.Application.Authorization
{
    public static class GroupPolicies
    {
        public const string ViewGroup = "GroupPolicy_View";
        public const string EditGroup = "GroupPolicy_Edit";
        public const string DeleteGroup = "GroupPolicy_Delete";
        public const string ManageMembers = "GroupPolicy_ManageMembers";
        public const string ViewMembers = "GroupPolicy_ViewMembers";

        public static void AddGroupPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(ViewGroup, policy => policy.Requirements.Add(new GroupOperationRequirement(GroupOperation.View)));
            options.AddPolicy(EditGroup, policy => policy.Requirements.Add(new GroupOperationRequirement(GroupOperation.Edit)));
            options.AddPolicy(DeleteGroup, policy => policy.Requirements.Add(new GroupOperationRequirement(GroupOperation.Delete)));
            options.AddPolicy(ManageMembers, policy => policy.Requirements.Add(new GroupOperationRequirement(GroupOperation.ManageMembers)));
            options.AddPolicy(ViewMembers, policy => policy.Requirements.Add(new GroupOperationRequirement(GroupOperation.ViewMembers)));
        }
    }

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
}
