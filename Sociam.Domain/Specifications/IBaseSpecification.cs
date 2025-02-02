using Sociam.Domain.Entities.common;
using System.Linq.Expressions;

namespace Sociam.Domain.Specifications;
public interface IBaseSpecification<TEntity> where TEntity : BaseEntity
{
    public List<Expression<Func<TEntity, object>>> Includes { get; set; }
    List<Expression<Func<TEntity, object>>> OrderBy { get; set; }
    List<Expression<Func<TEntity, object>>> OrderByDescending { get; set; }
    public Expression<Func<TEntity, bool>>? Criteria { get; set; }
    public bool IsPagingEnabled { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public bool IsTracking { get; set; }
}
