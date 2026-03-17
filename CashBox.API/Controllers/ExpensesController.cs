using CashBox.Core.DTOs.ExpenseDTOs;
using CashBox.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashBox.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost("getAllExpenses")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _expenseService.GetExpensesWithCategoryDtoAsync();
        return Ok(result);
    }

    [HttpPost("createExpense")]
    public async Task<IActionResult> Create(ExpenseCreateDto expenseDto)
    {
        var result = await _expenseService.AddWithDtoAsync(expenseDto);
        return Ok(result);
    }

    [HttpPost("updateExpense")]
    public async Task<IActionResult> Update(ExpenseUpdateDto updateDto)
    {
        await _expenseService.UpdateWithDtoAsync(updateDto);
        return Ok("Harcama başarıyla güncellendi.");
    }

    [HttpPost("removeExpense")]
    public async Task<IActionResult> Remove(int id)
    {
        var expense = await _expenseService.GetByIdAsync(id);
        if (expense == null) return NotFound("Harcama bulunamadı.");

        _expenseService.Remove(expense);
        return Ok("Harcama silindi.");
    }
}