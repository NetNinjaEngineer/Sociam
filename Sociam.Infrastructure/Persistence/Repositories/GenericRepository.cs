using Microsoft.EntityFrameworkCore;
using Sociam.Domain.Entities.common;
using Sociam.Domain.Interfaces;
using Sociam.Domain.Specifications;
using Sociam.Domain.Utils;

namespace Sociam.Infrastructure.Persistence.Repositories;
public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly ApplicationDbContext context = context;

    public async Task<IEnumerable<T>> GetAllAsync()
        => await context.Set<T>().ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id) => await context.Set<T>().FindAsync(id);

    public async Task<T?> GetBySpecificationAsync(IBaseSpecification<T> specification)
        => await SpecificationQueryEvaluator.BuildQuery(context.Set<T>(), specification).FirstOrDefaultAsync();

    public async Task<T?> GetBySpecificationAndIdAsync(IBaseSpecification<T> specification, Guid id)
        => await SpecificationQueryEvaluator.BuildQuery(context.Set<T>(), specification).FirstOrDefaultAsync(e => e.Id == id);

    public void Create(T entity) => context.Set<T>().Add(entity);

    public void Update(T entity) => context.Set<T>().Update(entity);

    public void Delete(T entity) => context.Set<T>().Remove(entity);

    public async Task<IEnumerable<T>> GetAllWithSpecificationAsync(IBaseSpecification<T> specification)
        => await SpecificationQueryEvaluator.BuildQuery(context.Set<T>(), specification).ToListAsync();


    public async Task<int> GetCountWithSpecificationAsync(IBaseSpecification<T> specification)
        => await SpecificationQueryEvaluator.BuildQuery(context.Set<T>(), specification).CountAsync();
}
