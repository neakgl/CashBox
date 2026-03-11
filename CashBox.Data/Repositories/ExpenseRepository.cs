using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CashBox.Data.Repositories;
public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<List<Expense>> GetExpensesWithCategoryAsync()
    {
        return await _context.Expenses.Include(x => x.Category).ToListAsync();
    }
}