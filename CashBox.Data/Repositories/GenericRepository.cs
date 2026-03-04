using CashBox.Core.Repositories;
using CashBox.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CashBox.Data.Repositories;

// Core katmanındaki IGenericRepository sözleşmesini imzalıyoruz!
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    // Veritabanı bağlantımız (Context)
    protected readonly AppDbContext _context;

    // Üzerinde işlem yapacağımız tablo (Category, Expense vb.)
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>(); // Hangi tablo gelirse onu ayarla
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // Tüm tabloyu listeye çevirip getir
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        // FindAsync, verilen ID'yi tablonun Primary Key'inde arar ve bulur
        return await _dbSet.FindAsync(id);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression);
    }
}