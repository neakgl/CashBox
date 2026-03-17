using AutoMapper;
using CashBox.Core.DTOs.IncomeDTOs;
using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Core.Services;
using CashBox.Core.UnitOfWorks;

namespace CashBox.Service.Services;

public class IncomeService : GenericService<Income>, IIncomeService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly IMapper _mapper;

    public IncomeService(IGenericRepository<Income> repository, IUnitOfWork unitOfWork, IIncomeRepository incomeRepository, IMapper mapper)
        : base(repository, unitOfWork)
    {
        _incomeRepository = incomeRepository;
        _mapper = mapper;
    }

    public async Task<IncomeDto> AddWithDtoAsync(IncomeCreateDto dto, int userId)
    {
        var income = _mapper.Map<Income>(dto);
        income.UserId = userId;
        // Eğer dto içinde Date null ise o anki zamanı basıyoruz
        income.Date = dto.Date != default ? dto.Date : DateTime.Now;

        await AddAsync(income);
        return _mapper.Map<IncomeDto>(income);
    }

    public async Task<IEnumerable<IncomeDto>> GetAllWithDtoAsync()
    {
        var incomes = await GetAllAsync();
        return _mapper.Map<IEnumerable<IncomeDto>>(incomes);
    }

    public async Task UpdateWithDtoAsync(IncomeUpdateDto dto)
    {
        var income = await GetByIdAsync(dto.Id);
        if (income != null)
        {
            _mapper.Map(dto, income);
            Update(income);
        }
    }

    public async Task<IEnumerable<IncomeDto>> GetIncomesWithUserDtoAsync(int userId)
    {
        var incomes = await _incomeRepository.GetIncomesWithUserAsync();
        var userIncomes = incomes.Where(x => x.UserId == userId).ToList();

        return _mapper.Map<IEnumerable<IncomeDto>>(userIncomes);
    }
}