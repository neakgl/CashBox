using AutoMapper;
using CashBox.Core.DTOs.ExpenseDTOs;
using CashBox.Core.Entities;
using CashBox.Core.Repositories;
using CashBox.Core.Services;
using CashBox.Core.UnitOfWorks;

namespace CashBox.Service.Services;

public class ExpenseService : GenericService<Expense>, IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IGenericRepository<Expense> repository, IUnitOfWork unitOfWork, IExpenseRepository expenseRepository, IMapper mapper)
        : base(repository, unitOfWork)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ExpenseDto> AddWithDtoAsync(ExpenseCreateDto dto)
    {
        var expense = _mapper.Map<Expense>(dto);
        await AddAsync(expense);
        return _mapper.Map<ExpenseDto>(expense);
    }

    public async Task<IEnumerable<ExpenseDto>> GetAllWithDtoAsync()
    {
        var expenses = await GetAllAsync();
        return _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
    }

    public async Task UpdateWithDtoAsync(ExpenseUpdateDto dto)
    {
        var expense = await GetByIdAsync(dto.Id);
        if (expense != null)
        {
            _mapper.Map(dto, expense);
            Update(expense);
        }
    }
}