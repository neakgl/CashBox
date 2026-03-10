using CashBox.Core.Entities;
using CashBox.Core.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost("getAllExpense")]
    public async Task<IActionResult> GetAll()
    {
        var expenses = await _expenseService.GetAllAsync();
        return Ok(expenses);
    }

    [HttpPost("createExpense")]
    public async Task<IActionResult> Create(Expense expense)
    {
        await _expenseService.AddAsync(expense);
        return Ok(expense);
    }

    [HttpPost("updateExpense")]
    public async Task<IActionResult> Update(Expense expense)
    {
        _expenseService.Update(expense);
        return Ok("Harcama güncellendi.");
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