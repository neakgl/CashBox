using CashBox.Core.DTOs.WalletDTOs;
using CashBox.Core.Services;

namespace CashBox.Service.Services;

public class WalletService : IWalletService
{
    private readonly IIncomeService _incomeService;
    private readonly IExpenseService _expenseService;

    public WalletService(IIncomeService incomeService, IExpenseService expenseService)
    {
        _incomeService = incomeService;
        _expenseService = expenseService;
    }

    public async Task<WalletSummaryDto> GetWalletSummaryAsync(int userId)
    {
        var incomes = await _incomeService.GetIncomesWithUserDtoAsync(userId);

        var expenses = await _expenseService.GetExpensesWithCategoryDtoAsync(userId);

        var totalIncome = incomes.Sum(x => x.Amount);
        var totalExpense = expenses.Sum(x => x.Amount);

        return new WalletSummaryDto
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense
        };
    }
}