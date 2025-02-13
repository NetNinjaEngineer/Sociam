using Sociam.Domain.Entities;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Specifications
{
    public sealed class GroupsWithFilterationCountSpecification(GroupParams @params) : BaseSpecification<Group>(g =>
        (string.IsNullOrEmpty(@params.SearchTerm) ||
         g.Name.ToLower().Contains(@params.SearchTerm.ToLower()) ||
         (g.Description != null && g.Description.ToLower().Contains(@params.SearchTerm.ToLower()))) &&
        (string.IsNullOrEmpty(@params.Name) || g.Name.ToLower() == @params.Name.ToLower()) &&
        (!@params.GroupPrivacy.HasValue || g.GroupPrivacy == @params.GroupPrivacy));
}
