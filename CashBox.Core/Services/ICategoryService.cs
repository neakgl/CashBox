using CashBox.Core.DTOs.CategoryDTOs;
using CashBox.Core.Entities;

namespace CashBox.Core.Services;

public interface ICategoryService : IGenericService<Category>
{
    Task<CategoryDto> AddWithDtoAsync(CategoryCreateDto dto);
    Task<IEnumerable<CategoryDto>> GetAllWithDtoAsync();
    Task UpdateWithDtoAsync(CategoryUpdateDto dto);
}