namespace CashBox.Core.DTOs.WalletDTOs;

public class WalletSummaryDto
{
    public decimal TotalIncome { get; set; } 
    public decimal TotalExpense { get; set; }
    public decimal NetBalance => TotalIncome - TotalExpense;
}
