using CashBox.Core.DTOs.CategoryDTOs;
using CashBox.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashBox.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("getAllCategories")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllWithDtoAsync();
        return Ok(result);
    }

    [HttpPost("createCategory")]
    public async Task<IActionResult> Create(CategoryCreateDto categoryDto)
    {
        var result = await _categoryService.AddWithDtoAsync(categoryDto);
        return Ok(result);
    }

    [HttpPost("updateCategory")]
    public async Task<IActionResult> Update(CategoryUpdateDto updateDto)
    {
        await _categoryService.UpdateWithDtoAsync(updateDto);
        return Ok("Kategori başarıyla güncellendi.");
    }

    [HttpPost("removeCategory")]
    public async Task<IActionResult> Remove(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound("Kategori bulunamadı.");

        _categoryService.Remove(category);
        return Ok("Kategori silindi.");
    }
}