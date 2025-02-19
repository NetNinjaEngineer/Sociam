using Sociam.Domain.Entities;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Specifications
{
    public sealed class GetAllGroupsByParamsSpecification : BaseSpecification<Group>
    {
        public GetAllGroupsByParamsSpecification(GroupParams @params) : base(
            g => (string.IsNullOrEmpty(@params.SearchTerm) ||
             g.Name.ToLower().Contains(@params.SearchTerm.ToLower()) ||
             (g.Description != null && g.Description.ToLower().Contains(@params.SearchTerm.ToLower()))) &&

                (string.IsNullOrEmpty(@params.Name) || g.Name.ToLower() == @params.Name.ToLower()) && // checks the Name provided in params if its null or empty returns true which means that the all expression will be ignored as we used OR operation

                (!@params.GroupPrivacy.HasValue || g.GroupPrivacy == @params.GroupPrivacy))
        {
            if (!string.IsNullOrEmpty(@params.Sort))
            {
                switch (@params.Sort)
                {
                    case "NameASC":
                        AddOrderBy(g => g.Name);
                        break;

                    case "NameDESC":
                        AddOrderByDescending(g => g.Name);
                        break;

                    case "CreatedAtASC":
                        AddOrderBy(g => g.CreatedAt);
                        break;

                    case "CreatedAtDESC":
                    default:
                        AddOrderByDescending(g => g.CreatedAt);
                        break;
                }
            }

            if (@params.EnablePaging)
                ApplyPaging(@params.Page, @params.PageSize);
        }
    }
}
