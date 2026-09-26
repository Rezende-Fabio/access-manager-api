using System.Linq.Expressions;

namespace access_manager_api.Application.Interfaces.RepositoryInt;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null);

    Task AddAsync(T entity);
    void AddRange(IEnumerable<T> entities);

    void Update(T entity);
    void Remove(T entity);
}