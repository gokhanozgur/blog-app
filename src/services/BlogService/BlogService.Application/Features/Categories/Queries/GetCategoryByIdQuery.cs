using BlogService.Application.DTOs.Categories;
using MediatR;

namespace BlogService.Application.Features.Categories.Queries;

public class GetCategoryByIdQuery : IRequest<CategoryDto>
{
    public string CategoryId { get; set; }
}