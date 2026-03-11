using AutoMapper;
using CashBox.Core.Entities;
using CashBox.Core.DTOs.CategoryDTOs;

namespace CashBox.Service.Mapping;
public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<CategoryCreateDto, Category>().ReverseMap();

        CreateMap<CategoryUpdateDto, Category>().ReverseMap();
    }
}