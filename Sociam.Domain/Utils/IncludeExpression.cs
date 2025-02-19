using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities.common;
using System.Linq.Expressions;

namespace Sociam.Domain.Utils;

public sealed class IncludeExpression<T>(
    Expression<Func<T, object>> includeExpression) : IIncludeExpression<T> where T : BaseEntity
{
    public IQueryable<T> AddInclude(IQueryable<T> query)
        => query.Include(includeExpression);
}