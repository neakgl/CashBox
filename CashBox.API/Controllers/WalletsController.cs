using CashBox.API.Extensions;
using CashBox.Core.DTOs.ResponseDTOs;
using CashBox.Core.DTOs.WalletDTOs;
using CashBox.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashBox.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WalletsController : CustomBaseController
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
        return CreateActionResult(CustomResponseDto<WalletSummaryDto>.Success(200, summary));
    }
}
