using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Data.Context;

namespace CashBox.Data.Repositories;
public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(AppDbContext context) : base(context)
    {
    }
}