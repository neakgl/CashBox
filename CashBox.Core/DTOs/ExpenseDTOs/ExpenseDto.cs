namespace CashBox.Core.DTOs.ExpenseDTOs;

public class ExpenseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }
}