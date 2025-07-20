using BlogService.Application.DTOs.Categories;
using MediatR;

namespace BlogService.Application.Features.Categories.Queries;

public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
{
    
}