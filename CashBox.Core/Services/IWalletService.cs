using CashBox.Core.DTOs.WalletDTOs;

namespace CashBox.Core.Services;

public interface IWalletService
{
    Task<WalletSummaryDto> GetWalletSummaryAsync(int userId);
}
