using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Core.Services;
using CashBox.Core.UnitOfWorks;

namespace CashBox.Service.Services;

public class CategoryService : GenericService<Category>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        : base(repository, unitOfWork)
    {
        _categoryRepository = categoryRepository;
    }
}