using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Data.Context;

namespace CashBox.Data.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
