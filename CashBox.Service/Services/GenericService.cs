using System.Linq.Expressions;
using CashBox.Core.Repositories;
using CashBox.Core.Services;
using CashBox.Core.UnitOfWorks;

namespace CashBox.Service.Services;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IUnitOfWork _unitOfWork;

    // DI
    public GenericService(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync(); 
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync(); //sadece reading
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public void Remove(T entity)
    {
        _repository.Remove(entity);
        _unitOfWork.Commit();     
    }

    public void Update(T entity)
    {
        _repository.Update(entity);
        _unitOfWork.Commit(); 
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _repository.Where(expression);
    }
}