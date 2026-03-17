using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CashBox.Data.Repositories;

public class IncomeRepository : GenericRepository<Income>, IIncomeRepository
{
    public IncomeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Income>> GetIncomesWithUserAsync()
    {
        return await _context.Incomes
            .Include(x => x.User)
            .ToListAsync();
    }
}
