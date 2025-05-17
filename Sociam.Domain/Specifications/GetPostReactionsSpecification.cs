using Sociam.Domain.Entities;
using Sociam.Domain.Utils;

namespace Sociam.Domain.Specifications;

public sealed class GetPostReactionsSpecification : BaseSpecification<PostReaction>
{
    public GetPostReactionsSpecification(Guid postId, PostReactionsParams @params) : base(pr => pr.PostId == postId)
    {
        AddIncludes(pr => pr.ReactedBy);

        if (!string.IsNullOrEmpty(@params.Sort))
        {
            switch (@params.Sort.ToLower())
            {
                case "newest":
                    AddOrderByDescending(pr => pr.ReactedAt);
                    break;
                case "oldest":
                    AddOrderBy(pr => pr.ReactedAt);
                    break;
                default:
                    AddOrderByDescending(pr => pr.ReactedAt);
                    break;
            }
        }
        else
            AddOrderByDescending(pr => pr.ReactedAt);

        ApplyPaging(@params.Page, @params.PageSize);
    }
}