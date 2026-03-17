using CashBox.API.Extensions;
using CashBox.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashBox.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WalletsController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletsController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpPost("getSummary")]
    public async Task<IActionResult> GetSummary()
    {
        var summary = await _walletService.GetWalletSummaryAsync(User.GetUserId());
        return Ok(summary);
    }
}
