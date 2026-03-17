namespace CashBox.Core.DTOs.IncomeDTOs;

public class IncomeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
}