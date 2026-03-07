using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Core.Services;
using CashBox.Core.UnitOfWorks;

namespace CashBox.Service.Services;

public class ExpenseService : GenericService<Expense>, IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IGenericRepository<Expense> repository, IUnitOfWork unitOfWork, IExpenseRepository expenseRepository)
        : base(repository, unitOfWork)
    {
        _expenseRepository = expenseRepository;
    }
}