using AutoMapper;
using CashBox.Core.DTOs.CategoryDTOs;
using CashBox.Core.DTOs.ExpenseDTOs;
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
    }
}