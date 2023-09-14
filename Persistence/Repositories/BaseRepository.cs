using System.Linq;
using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

public abstract class BaseRepository<T> : ApplicationDbContext<T>, IBaseRepository<T>
  where T : IEntity
{
    protected DbSet<T> Entity { get; init; }
    protected BaseRepository(DbContextOptions<ApplicationDbContext<T>> options) : base(options) => Entity = Set<T>();




    public async Task<T?> GetById<U>(int id) where U : IEntity
    {
        return await Entity.FindAsync(id);
    }

    public IQueryable<T> FindQueryable<U>(Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null) where U : IEntity
    {
        var query = Entity.Where(expression);
        return orderBy != null ? orderBy(query) : query;
    }

    public Task<List<T>> FindListAsync<U>(Expression<Func<T, bool>>? expression, Func<IQueryable<T>,
        IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default)
        where U : class
    {
        var query = expression != null ? Entity.Where(expression) : Entity;
        return orderBy != null
            ? orderBy(query).ToListAsync(cancellationToken)
            : query.ToListAsync(cancellationToken);
    }

    public Task<List<T>> FindAllAsync<U>(CancellationToken cancellationToken) where U : IEntity
    {
        return Entity.ToListAsync(cancellationToken);
    }

    public Task<T?> SingleOrDefaultAsync<U>(Expression<Func<T, bool>> expression, string includeProperties) where U : IEntity
    {
        var query = Entity.AsQueryable();

        query = includeProperties.Split(new char[] { ',' },
            StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty)
            => current.Include(includeProperty));

        return query.SingleOrDefaultAsync(expression);
    }

    public T Add<U>(T entity) where U : IEntity
    {
        return Entity.Add(entity).Entity;
    }

    public void Update<U>(T entity) where U : IEntity
    {
        Entity.Entry(entity).State = EntityState.Modified;
    }

    public void UpdateRange<U>(IEnumerable<T> entities) where U : IEntity
    {
        Entity.UpdateRange(entities);
    }

    public void Delete<U>(T entity) where U : IEntity
    {
        Entity.Remove(entity);
    }

}