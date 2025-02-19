using Sociam.Domain.Entities.common;

namespace Sociam.Domain.Utils;

public interface IIncludeExpression<T> where T : BaseEntity
{
    IQueryable<T> AddInclude(IQueryable<T> query);
}