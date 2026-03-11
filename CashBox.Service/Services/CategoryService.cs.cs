using AutoMapper;
using CashBox.Core.DTOs.CategoryDTOs;
using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Core.Services;
using CashBox.Core.UnitOfWorks;

namespace CashBox.Service.Services;

public class CategoryService : GenericService<Category>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IMapper mapper)
        : base(repository, unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<CategoryDto> AddWithDtoAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);

        await AddAsync(category);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllWithDtoAsync()
    {
        var categories = await GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task UpdateWithDtoAsync(CategoryUpdateDto dto)
    {
        var category = await GetByIdAsync(dto.Id);
        if (category != null)
        {
            _mapper.Map(dto, category);
            Update(category);
        }
    }
}