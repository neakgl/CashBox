namespace CashBox.Core.DTOs.UserDTOs;

public class UserUpdateDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

}