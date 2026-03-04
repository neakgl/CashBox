using System.Linq.Expressions;


namespace CashBox.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    // Read İşlemleri
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> Where(Expression<Func<T, bool>> expression); 

    // Create İşlemi
    Task AddAsync(T entity);

    // Update İşlemi
    void Update(T entity);

    // Delete İşlemi
    void Remove(T entity);
}
