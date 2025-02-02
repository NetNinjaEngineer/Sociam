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
}
