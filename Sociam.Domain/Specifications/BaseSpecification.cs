using Sociam.Domain.Entities.common;
using Sociam.Domain.Utils;
using System.Linq.Expressions;

namespace Sociam.Domain.Specifications;
public abstract class BaseSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : BaseEntity
{
    public bool IsTracking { get; private set; } = true;
    public bool IsPagingEnabled { get; private set; }
    public int Skip { get; private set; }
    public int Take { get; private set; }
    public List<IIncludeExpression<TEntity>> IncludeExpressions { get; } = [];
    public List<Expression<Func<TEntity, object>>> OrderBy { get; } = [];
    public List<Expression<Func<TEntity, object>>> OrderByDescending { get; } = [];
    public Expression<Func<TEntity, bool>>? Criteria { get; }

    protected BaseSpecification() { }

    protected BaseSpecification(Expression<Func<TEntity, bool>> criteriaExpression)
        => Criteria = criteriaExpression;

    protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression)
        => IncludeExpressions.Add(new IncludeExpression<TEntity>(includeExpression));

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
       => OrderBy.Add(orderByExpression);

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescending.Add(orderByDescendingExpression);

    protected void DisableTracking() => IsTracking = false;

    protected void ApplyPaging(int skip, int take)
    {
        IsPagingEnabled = true;
        Skip = skip;
        Take = take;
    }

    protected void AddInclude<TPreviousProperty, TProperty>(
        Expression<Func<TEntity, IEnumerable<TPreviousProperty>>> previousExpression,
        Expression<Func<TPreviousProperty, TProperty>> thenExpression)
    {
        IncludeExpressions.Add(
            new ThenIncludeExpression<TEntity, TPreviousProperty, TProperty>(
                previousExpression, thenExpression));
    }


}
