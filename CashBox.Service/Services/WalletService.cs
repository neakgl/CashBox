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

        var today = DateTime.Today;
        // Haftanın başını Pazartesi kabul ediyoruz
        int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        var startOfWeek = today.AddDays(-1 * diff).Date;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);

        var totalIncome = incomes.Sum(x => x.Amount);
        var totalExpense = expenses.Sum(x => x.Amount);

        var categoryPercentages = new List<CategoryExpensePercentageDto>();
        if (totalExpense > 0)
        {
            categoryPercentages = expenses
                .GroupBy(x => string.IsNullOrWhiteSpace(x.CategoryName) ? "Diğer" : x.CategoryName)
                .Select(g => new CategoryExpensePercentageDto
                {
                    CategoryName = g.Key,
                    TotalAmount = g.Sum(x => x.Amount),
                    // Yüzdeyi virgülden sonra 2 basamağa yuvarlıyoruz
                    Percentage = Math.Round((g.Sum(x => x.Amount) / totalExpense) * 100m, 2)
                })
                .OrderByDescending(x => x.Percentage) // En çok harcanan kategori en üstte
                .ToList();
        }

        return new WalletSummaryDto
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            DailyExpense = expenses.Where(x => x.Date.Date == today).Sum(x => x.Amount),
            WeeklyExpense = expenses.Where(x => x.Date.Date >= startOfWeek).Sum(x => x.Amount),
            MonthlyExpense = expenses.Where(x => x.Date.Date >= startOfMonth).Sum(x => x.Amount),

            MonthlyIncome = incomes.Where(x => x.Date.Date >= startOfMonth).Sum(x => x.Amount),

            CategoryPercentages = categoryPercentages
        };
    }
}