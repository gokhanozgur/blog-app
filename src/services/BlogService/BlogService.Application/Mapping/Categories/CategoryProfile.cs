using AutoMapper;
using BlogService.Application.DTOs.Categories;
using BlogService.Domain.Entities;

namespace BlogService.Application.Mapping.Categories;

public class CategoryProfile :  Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}