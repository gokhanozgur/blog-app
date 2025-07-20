using BlogService.Application.DTOs.Categories;
using MediatR;

namespace BlogService.Application.Features.Categories.Commands;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public CreateCategoryDto CreateCategoryDto { get; set; }
    public CreateCategoryCommand(CreateCategoryDto dto) =>  CreateCategoryDto = dto;
}