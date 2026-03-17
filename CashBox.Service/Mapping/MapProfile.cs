using AutoMapper;
using CashBox.Core.DTOs.CategoryDTOs;
using CashBox.Core.DTOs.ExpenseDTOs;
using CashBox.Core.DTOs.IncomeDTOs;
using CashBox.Core.DTOs.UserDTOs;
using CashBox.Core.Entities;

namespace CashBox.Service.Mapping;
public class MapProfile : Profile
{
    public MapProfile()
    {
        //category
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<CategoryUpdateDto, Category>().ReverseMap();

        //expense
        CreateMap<Expense, ExpenseDto>().ReverseMap();
        CreateMap<ExpenseCreateDto, Expense>().ReverseMap();
        CreateMap<ExpenseUpdateDto, Expense>().ReverseMap();

        //user
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();

        //income
        CreateMap<Income, IncomeDto>().ReverseMap();
        CreateMap<IncomeCreateDto, Income>();
        CreateMap<IncomeUpdateDto, Income>();
    }
}