using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CashBox.Data.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
    }
}
