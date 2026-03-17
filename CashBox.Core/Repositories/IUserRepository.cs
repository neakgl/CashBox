using CashBox.Core.Entities;

namespace CashBox.Core.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}