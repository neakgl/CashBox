namespace CashBox.Core.DTOs.ExpenseDTOs;

public class ExpenseUpdateDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
}