using BlogService.Application.DTOs.Categories;
using MediatR;

namespace BlogService.Application.Features.Categories.Commands;

public class UpdateCategoryCommand : IRequest<CategoryDto>
{
    public string CategoryId { get; set; }
    public UpdateCategoryDto UpdateCategoryDto { get; set; }

    public UpdateCategoryCommand(string id, UpdateCategoryDto dto)
    {
        CategoryId = id;
        UpdateCategoryDto = dto;
    }
}