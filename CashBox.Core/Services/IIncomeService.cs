using CashBox.Core.DTOs.IncomeDTOs;
using CashBox.Core.Entities;

namespace CashBox.Core.Services;

public interface IIncomeService : IGenericService<Income>
{
    Task<IncomeDto> AddWithDtoAsync(IncomeCreateDto dto, int userId);
    Task<IEnumerable<IncomeDto>> GetAllWithDtoAsync();
    Task UpdateWithDtoAsync(IncomeUpdateDto dto);
    Task<IEnumerable<IncomeDto>> GetIncomesWithUserDtoAsync(int userId);
}
