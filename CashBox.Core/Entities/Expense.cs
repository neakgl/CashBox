using CashBox.Core.Entities.Common;

namespace CashBox.Core.Entities;

public class Expense : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    //every expense has a category
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
