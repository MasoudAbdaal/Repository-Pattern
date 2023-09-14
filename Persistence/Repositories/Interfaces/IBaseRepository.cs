using System.Linq.Expressions;
using Domain.Entities;

internal interface IBaseRepository<T>
where T : IEntity
{
    Task<T?> GetById<U>(int id) where U : IEntity;
    IQueryable<T> FindQueryable<U>(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null) where U : IEntity;
    Task<List<T>> FindListAsync<U>(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default) where U : class;
    Task<List<T>> FindAllAsync<U>(CancellationToken cancellationToken) where U : IEntity;
    Task<T?> SingleOrDefaultAsync<U>(Expression<Func<T, bool>> expression, string includeProperties) where U : IEntity;
    T Add<U>(T entity) where U : IEntity;
    void Update<U>(T entity) where U : IEntity;
    void UpdateRange<U>(IEnumerable<T> entities) where U : IEntity;
    void Delete<U>(T entity) where U : IEntity;
}