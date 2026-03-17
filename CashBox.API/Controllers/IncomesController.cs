using CashBox.API.Extensions;
using CashBox.Core.DTOs.IncomeDTOs;
using CashBox.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashBox.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class IncomesController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomesController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpPost("getAllIncomes")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _incomeService.GetIncomesWithUserDtoAsync(User.GetUserId());
        return Ok(result);
    }

    [HttpPost("createIncome")]
    public async Task<IActionResult> Create(IncomeCreateDto incomeDto)
    {
        var result = await _incomeService.AddWithDtoAsync(incomeDto, User.GetUserId());
        return Ok(result);
    }

    [HttpPost("updateIncome")]
    public async Task<IActionResult> Update(IncomeUpdateDto updateDto)
    {
        await _incomeService.UpdateWithDtoAsync(updateDto);
        return Ok("Gelir başarıyla güncellendi.");
    }

    [HttpPost("removeIncome")]
    public async Task<IActionResult> Remove(int id)
    {
        var income = await _incomeService.GetByIdAsync(id);
        if (income == null) return NotFound("Gelir bulunamadı.");

        _incomeService.Remove(income);
        return Ok("Gelir silindi.");
    }
}