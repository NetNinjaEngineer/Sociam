using Sociam.Domain.Entities.common;
using Sociam.Domain.Utils;
using System.Linq.Expressions;

namespace Sociam.Domain.Specifications;
public interface IBaseSpecification<TEntity> where TEntity : BaseEntity
{
    public List<IIncludeExpression<TEntity>> IncludeExpressions { get; }
    List<Expression<Func<TEntity, object>>> OrderBy { get; }
    List<Expression<Func<TEntity, object>>> OrderByDescending { get; }
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public bool IsPagingEnabled { get; }
    public int Skip { get; }
    public int Take { get; }
    public bool IsTracking { get; }
}
