namespace CashBox.Core.DTOs.WalletDTOs;

public class WalletSummaryDto
{
    public decimal TotalIncome { get; set; } 
    public decimal TotalExpense { get; set; }
    public decimal NetBalance => TotalIncome - TotalExpense;

    public decimal DailyExpense { get; set; }
    public decimal WeeklyExpense { get; set; } 
    public decimal MonthlyExpense { get; set; }
    public decimal MonthlyIncome { get; set; }
    public List<CategoryExpensePercentageDto> CategoryPercentages { get; set; } = new();

}
public class CategoryExpensePercentageDto
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal Percentage { get; set; }
}
