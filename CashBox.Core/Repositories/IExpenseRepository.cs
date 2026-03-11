using CashBox.Core.Entities;

namespace CashBox.Core.Repositories;

public interface IExpenseRepository : IGenericRepository<Expense>
{
    Task<List<Expense>> GetExpensesWithCategoryAsync();
}