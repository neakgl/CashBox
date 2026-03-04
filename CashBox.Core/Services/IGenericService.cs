using System.Linq.Expressions;


namespace CashBox.Core.Services;

public interface IGenericService<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}