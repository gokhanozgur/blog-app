using BlogService.Application.DTOs.Categories;
using FluentValidation;

namespace BlogService.Application.Validators.Categories;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryCommandValidator()
    {
        
    }
}