using CashBox.Core.Enums;

namespace CashBox.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.StandardUser;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public ICollection<Expense> Expenses { get; set; }
}