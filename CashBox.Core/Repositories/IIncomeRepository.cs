using CashBox.Core.Entities;

namespace CashBox.Core.Repositories;

public interface IIncomeRepository : IGenericRepository<Income>
{
    Task<List<Income>> GetIncomesWithUserAsync();
}
