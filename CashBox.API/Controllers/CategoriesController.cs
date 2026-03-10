using CashBox.Core.Entities;
using CashBox.Core.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("getAllCategory")]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpPost("createCategory")]
    public async Task<IActionResult> Create(Category category)
    {
        await _categoryService.AddAsync(category);

        return Ok(category);
    }

    [HttpPost("updateCategory")]
    public async Task<IActionResult> Update(Category category)
    {
        _categoryService.Update(category);
        return Ok("Kategori güncellendi.");
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
