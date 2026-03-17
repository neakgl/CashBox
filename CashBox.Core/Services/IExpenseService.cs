using CashBox.Core.DTOs.ExpenseDTOs;
using CashBox.Core.Entities;

namespace CashBox.Core.Services;

public interface IExpenseService : IGenericService<Expense>
{
    Task<ExpenseDto> AddWithDtoAsync(ExpenseCreateDto dto, int userId);
    Task<IEnumerable<ExpenseDto>> GetAllWithDtoAsync();
    Task UpdateWithDtoAsync(ExpenseUpdateDto dto);
    Task<IEnumerable<ExpenseDto>> GetExpensesWithCategoryDtoAsync(int userId);
}
