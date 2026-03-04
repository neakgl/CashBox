using CashBox.Core.UnitOfWorks;
using CashBox.Data.Context;

namespace CashBox.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void Commit() //sync
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync() //async
    {
        await _context.SaveChangesAsync(); 
    }
}
