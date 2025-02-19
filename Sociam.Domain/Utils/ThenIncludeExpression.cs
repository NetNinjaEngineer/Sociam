using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities.common;

namespace Sociam.Domain.Utils;

public class ThenIncludeExpression<TEntity, TPreviousProperty, TProperty>(
    Expression<Func<TEntity, IEnumerable<TPreviousProperty>>> previousExpression,
    Expression<Func<TPreviousProperty, TProperty>> thenExpression)
    : IIncludeExpression<TEntity>
    where TEntity : BaseEntity
{
    public IQueryable<TEntity> AddInclude(IQueryable<TEntity> query)
    {
        return query.Include(previousExpression).ThenInclude(thenExpression);
    }
}