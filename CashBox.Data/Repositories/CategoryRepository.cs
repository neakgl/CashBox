using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Data.Context;

namespace CashBox.Data.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
